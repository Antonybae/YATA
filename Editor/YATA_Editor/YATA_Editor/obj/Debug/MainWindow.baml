   M S B A M L   `   `   `  ����F  BYATA_Editor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:YATA_EditorYATA_Editor  5   	       YATA_Editor.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:YATA_Editor     5   	     � Title$  
MainWindow��5   	   $	��600��6   $	��800��6)    � 	Resources 5
         MyButton �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value��%�  &	        �� 5   
   " ��6!   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $��border��6!   $��0q�6/   
���   �#��z��6W   ._�5      �� $��Center=�6/   $��Center=�6L   5       5      P� 5       �@J�IsMouseOver . . �5   "   ��  �@J�Opacity$	 0.8Q�6<   5      5      5      5      5      .��5      � .+�5    
   ]� .+�5!      o� $	��Top=�6   ���	#FFCDCDCD.@�5"      m� $��_File��6   
�������.@�5#      m� -   5        $
��_New��5#      6-   m� 5$      -   5        $��_Open��5$      6.   m� 5%      -   5        $��_Save��5%      6.   m� 5&      -   5        $��_Save as...��5&      64   5'      m� 5(      $��_Edit��6   
�������.@�5)      m� -   5        $��_Configuration��5)      67   5*      m� 5+      $��_About��6   
�������6+   5-      �� 5.      6   5/      &   System.Windows.Controls.DataGrid  50   
   -   5        $��
scriptGrid��50      $��
0,38,10,10q�6&   	   AutoGenerateColumns	 .  $��Right=�6V   $	��563��6r   
   Columns
 51      0  *System.Windows.Controls.DataGridTextColumn  52      ,  &System.Windows.Controls.DataGridColumn   Header$ Command��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 430 66      CanUserResize .  1  +System.Windows.Controls.DataGridBoundColumn   Binding 6B   �� *[0]+6Z   4  .System.Windows.Controls.DataGridTemplateColumn  53      $ Image��6)   $	 120 68      
IsReadOnly .  .     CellTemplate 54      �� 55      .R�56      �� ��6$   �� *[1]+57      59      5;      �� 5<   
   -   5        $��operationsListBox��5<      $	��521��6+   $��10,38,633,0q�68   $	��Top=�6M   ��5=      �� 5>      .R�5?      ��  �� Orientation$ 
Horizontal=�6!   .+�5@      �� $��3q�6    $��25��6D    � ToolTip 6+   �� *Name+��6O   �� *Icon+�� 5A      $��3q�6$   ��6/   �� *Value+5B      5C      5E      �� 5F   
   -   5        $��0,220,578,0q�5F   ,   $	��Top=�6A   $��50��6Y   
���   �#��[  6x   $��Right=�6�   $��50��6�   ��5G      �� 5H       �� ImageSource$ Icons/chevron_blue.png��6   5J      �� 5K   
   -	   5        $��0,275,578,0q�5K   /   $	��Top=�6D   $��50��6\   #��[  6h   $��Right=�6�   $��50��6�   ��5L      �� 5M      $ Icons/chevron_blue.png��6    �� RelativeTransform 5N      V� 5O      .~�5P      ��  �� CenterY$	 0.5Q�6-    �� CenterX$	 0.5Q�6;   �� 5Q       �� CenterY$	 0.5Q�6,    �� CenterX$	 0.5Q�6:   �� 5R       �� Angle$	 180Q�6.    �� CenterY$	 0.5Q�6:    �� CenterX$	 0.5Q�6H   T� 5S      5T      5V      5X      5Y      5Z      