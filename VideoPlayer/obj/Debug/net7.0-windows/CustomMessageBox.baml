   M S B A M L   `   `   `  ����  VideoPlayer"    VideoPlayer.CustomMessageBox   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Message��5   	   $	��200��5   	   $	��400��5   	    � WindowStartupLocation$ CenterOwner=�5   	    � 
ResizeMode$ NoResize=�5   	   ���#282c34 � WindowStyle$
 None=�5
   	    � AllowsTransparency$
 True��5   	    � 	Resources 5       
  UIFont �� 
TargetType �� Property �� Value	 �� Triggers
 P� Property P� Value��%�  &	        '�� F     7   '�� �     7   *�5   
   2.pack://application:,,,/Fonts/#Fira Code Retina)�6T   �� 5   
   " ��6   .�5      ��  � ��8   6+   �� 5       � �� �#DCDCDC6+   5      �� 5   
   " ��6   .�5      ��  � ��8   6+   �� 5       � �� �#21252b6+   �� 5       � �� �#DCDCDC6+   �� 5       � ��$ 0q�60   �� 5       � ��$
 10,5q�6(   	 5      P� 5      
 �@J�IsMouseOver$
 True��61   . �5      ��  � �� �#2c313a63   5      P� 5       
 � ��$
 True��6/   . �5!      ��  � �� �#1d1f2363   5"      5$      5%      .��5'      �� $��1q�6   ���#1d1f23._�5(   
   � .+�5*      � $��32��6   $	��Top=�6   ���#21252b.+�5+      �� -   5         ��Name$ 	TitleText��5+      $��Center=�6/   $��10,0,0,0q�6J   �� 5,      -   5        $��×��5,      $��45��6%   $��32��60   $��Right=�5-      5.      5/      � 52      $��20,42,20,20q�6    � RowDefinitions 53      �� 54      $��* �6$   �� 55      $
��Auto �6$   .+�59      �� -   5        $ MessageText��59      $
��Wrap=�5:      $��Center=�5;      �� 5>      $��1��6    �� Orientation$ 
Horizontal=�5?      $��Right=�5@      $��0,20,0,0q�5A      .+�5B      �� -   5         ��Name$ OkButton��5B      $��OK��5C      $��75��5D      $��0,0,10,0q�5F      �� 5G      -   5        $ CancelButton��5G      $��Cancel��5H      $��75��5I      $y�	Collapsed=�5K      5L      5M      5N      5O      5P      