   M S B A M L   `   `   `  ����  VideoPlayer%    VideoPlayer.ExportOptionsWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Export Options��5   	   $	��200��5   	   $	��400��5   	    � WindowStartupLocation$ CenterOwner=�5   	    � 
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
   � .+�5*      � $��32��6   $	��Top=�6   ���#21252b.+�5+      �� $��Export Options��6   $��Center=�62   $��10,0,0,0q�6M   �� 5,      -   5        $��×��5,      $��45��6%   $��32��60   $��Right=�5-      5.      5/      � 52      $��10,42,10,10q�6    � RowDefinitions 53      �� 54      $
��Auto �6$   �� 55      $��* �6$   �� 56      $
��Auto �6$   .+�5:      �� $��Export Format��6   $
��Bold"�61   $��0,0,0,10q�6C   �� 5=      $��2��6    �� Orientation$ 
Horizontal=�6*   $��Right=�6C   $��0,20,0,0q�6_   .+�5>      �� -   5        $��Export��5>      $��75��6.   $��0,0,10,0q�6T   �� 5?      -   5        $��Cancel��5?      $��75��6.   69   5@      5A      5B      5C      5D      