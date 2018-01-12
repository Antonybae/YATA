#main.py
from tkinter import*
from tkinter import ttk
import os
import cv2 as cv
import threading
from mss import mss
import pyautogui
from enum import Enum

class State(Enum):
    FIND = 1,
    CLICK = 2

class Searcher(): 
    def run(small, large, state):
        method = cv.TM_SQDIFF_NORMED
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
        if state == State.CLICK:
            #return position and deltas
            return ((MPx,MPy),(tcols,trows))
        elif state == State.FIND:
            # Display the original image with the rectangle around the match.
            cv.imshow('output',large_image)
            # The image is only displayed if we call this
            cv.waitKey(0)
            return True

class Clicker():
    def run(position):
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

class Interpretator():
    def __init__(self, string, logger):
        self.m_logger = logger
        self.m_string = string
    def process(self):
        #for each string in list of strings check command word and send to searcher
        for member in self.m_string:
            #command word clicking
            path = Screen.update()
            if "CLICK" in member.split():
                #check if end of file
                if "\n" in member:
                    temp = member[6:-1]
                else:
                    temp = member[6:]
                if(Clicker.run(Searcher.run(temp, path, State.CLICK))==True):                        
                    self.m_logger(member[:-1] + ' = Success!\n')
                else:
                    self.m_logger(member[:-1] + ' = Error\n')
            #command word for find
            elif "FIND" in member:
                #check if end of file
                if "\n" in member:
                    temp = member[5:-1]
                else:
                    temp = member[5:]
                if (Searcher.run(temp, path, State.FIND)==True):
                    self.m_logger(member[:-1] + ' = Success!\n')
                else:
                    self.m_logger(member[:-1] + ' = Not Found\n')
            #command word for double click
            elif "DOUBLECLICK" in member:
                if "\n" in member:
                    temp = member[12:-1]
                else:
                    temp = member[12:]
                pos = Searcher.run(temp, path, State.CLICK)
                if(Clicker.run(pos)==True):
                    Clicker.run(pos)            
                    self.m_logger(member[:-1] + ' = Success!\n')
                else:
                    self.m_logger(member[:-1] + ' = Not Found\n')

class Screen():
    #create current screen picture
    def update():
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
        #create widgets
        ttk.Label(self.window, text='Enter path to script: ').grid(column=1, row=1, sticky=(W,E))
        self.scriptPathEntry = ttk.Entry(self.window, width=50, textvariable=self.scriptPath).grid(column=2, row=1, sticky=(E,S))
        ttk.Button(self.window, text="Start", command=self.startScript).grid(column=3,row=3,sticky=W)
        self.useDefaulf = ttk.Checkbutton(self.window, text='Use default path', variable=self.scriptPath, onvalue='script.txt', offvalue='').grid(column=3,row=1, sticky=(E,S))
        self.log = Text(self.window, state='disabled', width=40, height=10)
        self.log.grid(column=1, row=4, columnspan=3, sticky=(N,S,E,W))
        
    def logTo(self, message):
        #enable text widget
        self.log['state'] = 'normal'
        #write message
        self.log.insert('1.0', message)
        #disable text widget
        self.log['state'] = 'disabled'
        
    def startScript(self):
        try:
            #checks script path
            self.logTo('Starting\n')
            self.read(self.scriptPath)
        except FileNotFoundError:
            self.logTo('File not found\n')
            
    def read(self, path):
        #opens script file as list of strings and send to interpretator
        with open(path.get(), 'r') as script:
            data = script.readlines()
            interpr =  Interpretator(data, self.logTo)
            interpr.process()

#main
if __name__ == '__main__':
    root = Tk()
    app = Application(root)
    root.mainloop()
