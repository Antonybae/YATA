#main.py
from tkinter import*
from tkinter import ttk
from tkinter import messagebox
import os
import cv2 as cv
import numpy as np
from mss import mss
import pyautogui
from enum import Enum
import time


class Checker:
    def __init__(self, path, logger, scriptPath = None):
        self.m_logger = logger
        self.m_path = path
        self.m_scriptPath = scriptPath
    #Checks if folder exists
    def CheckFolder(self):
        if os.path.isdir(self.m_path):
            return self.m_path
        else:
            self.m_logger(self.m_path + " Folder doesn't exist\n")
    #Checks if file exists
    def CheckFile(self):
        if os.path.isfile(self.m_path) :
            return self.m_path
        else:
            if self.CheckRelative():
                return self.relativePath
            else:
                self.m_logger(self.relativePath + " Doesn't exist\n")
    #Checks relative path if so returns it
    def CheckRelative(self):
        self.relativePath = os.path.dirname(os.path.abspath(self.m_scriptPath)) + "\\" + self.m_path
        if os.path.isfile(self.relativePath):
            return True

#basic operations list
class Operation(Enum):
    FIND = 1,
    CLICK = 2,
    DOUBLECLICK = 3
    UNTIL = 4

class Searcher(): 
    def Compare(small, large, need=0.1):
        small_image = cv.imread(small)
        large_image = cv.imread(large)
        result = cv.matchTemplate(small_image, large_image, cv.TM_SQDIFF_NORMED)
        mn,mx,mnLoc,_ = cv.minMaxLoc(result)
        print("min= "+str(mn))
        if(mn >= need):
            return False
        else:
            return True
    def Run(small, large, state, method = cv.TM_SQDIFF_NORMED):
        # Read the images from the file
        small_image = cv.imread(small)
        large_image = cv.imread(large)
        result = cv.matchTemplate(small_image, large_image, method)
        # We want the minimum squared difference
        mn,_,mnLoc,_ = cv.minMaxLoc(result)
        # Extract the coordinates of our best match
        MPx,MPy = mnLoc
        # Step 2: Get the size of the template. This is the same size as the match.
        trows,tcols = small_image.shape[:2]
        # Step 3: Draw the rectangle on large_image
        cv.rectangle(large_image, (MPx,MPy),(MPx+tcols,MPy+trows),(0,0,255),2)
        if state == Operation.CLICK:
            #return position and deltas
            return ((MPx,MPy),(tcols,trows))
        elif state == Operation.FIND:
            if result is not None:
                # Display the original image with the rectangle around the match.
                cv.imshow('output',large_image)
                # The image is only displayed if we call this
                cv.waitKey(0)
                return True
            else:
                return False

class MouseManager():
    def Click(position):
        #calculate middle of rectangle position
        middle = (position[0][0] + (position[1][0]/2), position[0][1]+(position[1][1]/2))
        #check if coordinates is null
        if middle is None:
            return False
        else:
            #move mouse to recognized position and click
            pyautogui.moveTo(middle)
            pyautogui.click(middle)
            return True
    def Paste(text):
        #create temporal clipboard
        temp = Tk()
        temp.withdraw()
        temp.clipboard_clear()
        temp.clipboard_append(text)
        #paste
        pyautogui.hotkey('ctrl', 'v') 
        #destroy clipboard
        temp.destroy()

class Interpretator():
    def __init__(self, string, scriptPath, logger):
        self.m_logger = logger
        self.m_string = string
        self.m_script = scriptPath

    def SendToSearcher(self, remove, member, operation):
        #check is path is right
        self.m_checker = Checker(member[remove:], self.m_logger, self.m_script)
        #create and copy relative path
        relative = self.m_checker.CheckFile()
        #check if exist once
        if Searcher.Compare(relative,self.ScreenshotPath) == True:
            #proceed needed operation
            if operation == Operation.CLICK:
                MouseManager.Click(Searcher.Run(relative, self.ScreenshotPath, operation))
                self.m_logger(member + ' = Success!\n')
            elif operation == Operation.FIND:
                Searcher.Run(relative, self.ScreenshotPath, operation)
                self.m_logger(member + ' = Success!\n')
                return True
            elif operation == Operation.UNTIL:
                Searcher.Run(relative, self.ScreenshotPath, operation)
                self.m_logger(member + ' = Success!\n')
                return True
            elif operation == Operation.DOUBLECLICK:
                position = Searcher.Run(relative, self.ScreenshotPath, Operation.CLICK)
                MouseManager.Click(position)
                MouseManager.Click(position)
                self.m_logger(member + ' = Success!\n')
        else:
            self.m_logger(member + ' = Error\n')
            return False

    def Process(self):
        #for each string in list of strings check command word and send to searcher
        for member in self.m_string:
            #command word clicking
            self.ScreenshotPath = Screen.Update()
            if "CLICK" in member.split():
                self.SendToSearcher(6, member, Operation.CLICK)
            #command word for find
            elif "FIND" in member.split():
                self.SendToSearcher(5, member, Operation.FIND)
            #command word for double click
            elif "DOUBLECLICK" in member.split():
                self.SendToSearcher(12, member, Operation.DOUBLECLICK)
            #command word for waiting
            elif "WAIT" in member.split():
                time.sleep(int(member[5:]))
                self.m_logger(member + ' = Success!\n')
            #command word for waiting until images matched
            elif "UNTIL" in member.split():
                while self.SendToSearcher(6, member, Operation.UNTIL) == False:
                    time.sleep(1)
                    self.m_logger('Waiting 1 sec\n')
                    self.ScreenshotPath = Screen.Update()
                self.m_logger('Done\n')
            #command word for pasting text
            elif "PASTE" in member.split():
                MouseManager.Paste(member[6:])
                self.m_logger(member[6:] + ' = Success!\n')
            elif "COMPARE" in member.split():
                result = self.SendToSearcher(8, member, Operation.UNTIL)
                if result == True:
                    messagebox.showinfo(message='Result is EQUAL')
                    self.m_logger("Result is EQUAL\n")
                else:
                    messagebox.showinfo(message='Result is NOT equal')
                    self.m_logger("Result is NOT equal\n") 
                
class Screen():
    #create current screen picture
    def Update():
        with mss() as sct:
            return sct.shot(output='screen.png')
            

class Application:
    def __init__(self, master):
        #configure main window
        master.title('YATA')
        self.window = ttk.Frame(master, padding="3 3 12 12")
        self.window.grid(column=0, row=0, sticky=(N, W, E, S))
        self.window.columnconfigure(0,weight=1)
        self.window.rowconfigure(0,weight=1)
        #widgets variables
        self.scriptPath = StringVar()
        self.folder = BooleanVar()
        #create widgets
        #label
        ttk.Label(self.window, text='Enter path to script: ').grid(column=1, row=1, sticky=(W,E))
        #textbox
        self.scriptPathEntry = ttk.Entry(self.window, width=50, textvariable=self.scriptPath).grid(column=2, row=1, sticky=(E,S))
        #button
        ttk.Button(self.window, text="Start", command=self.StartScript).grid(column=3,row=3,sticky=W)
        #checkbox isDefault
        self.useDefaulfCheckbox = ttk.Checkbutton(self.window, text='Use default path', variable=self.scriptPath, onvalue='Example\script.txt', offvalue='').grid(column=3,row=1, sticky=(W,E))
        #checkbox isFolder
        self.useFolderCheckbox = ttk.Checkbutton(self.window, text='Use folder with multiple scripts', variable=self.folder, onvalue=True, offvalue=False).grid(column=3,row=2, sticky=(E,S))
        #text 
        self.logText = Text(self.window, state='disabled', width=40, height=10)
        self.logText.grid(column=1, row=4, columnspan=3, sticky=(N,S,E,W))
        
    def LogTo(self, message):
        #enable text widget
        self.logText['state'] = 'normal'
        #write message
        self.logText.insert('1.0', message)
        #disable text widget
        self.logText['state'] = 'disabled'
        
    def StartScript(self):
        #create checker
        self.m_checker = Checker(self.scriptPath.get(), self.LogTo)
        if self.folder.get() == False:
            #checks script path
            if self.m_checker.CheckFile():
                self.LogTo('Starting\n')
                self.Read()
        else:
            #check script folder
            if self.m_checker.CheckFolder():
                self.LogTo('Starting\n')
                self.Read()
    
    def ProcessFile(self, path):
        #open script file
        with open(path, 'r') as script:
                data = [line.rstrip('\n') for line in script]
                interpr =  Interpretator(data, path, self.LogTo)
                interpr.Process()

    def Read(self):
        #opens script file as list of files
        if self.folder.get() == False:
            self.ProcessFile(self.scriptPath.get())
        else:
            for file in os.listdir(self.scriptPath.get()):
                if file.endswith(".txt"):
                    self.ProcessFile(self.scriptPath.get() + file)

#main
if __name__ == '__main__':
    root = Tk()
    app = Application(root)
    root.mainloop()
