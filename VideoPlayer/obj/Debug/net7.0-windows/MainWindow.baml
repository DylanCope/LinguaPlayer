   M S B A M L   `   `   `  ����F  BVideoPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:VideoPlayerVideoPlayer  5   	       VideoPlayer.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:VideoPlayer     5   	     � Title$'  !Video Player with Subtitle Editor��5   	   $	��720��5	   	   $
��1280��5
   	   ���#282c34 � WindowStyle$
 None=�5   	    � AllowsTransparency$
 True��5   	    � 
ResizeMode$ CanResizeWithGrip=�5   	    � 	Resources 5          BooleanToVisibilityConverter 
 UIFont  SubtitleFont  VideoSliderStyle �� 
TargetType �� Property �� Value �� 
TargetType	 � RowDefinitions
 ��Name �� CornerRadius ]�Name ]� DecreaseRepeatButton �� DecreaseLarge ]� IncreaseRepeatButton �� IncreaseLarge ]� Thumb  ModernScrollBarThumb �� RadiusX �� RadiusY  ModernScrollBar �� Triggers P� Property P� Value �� 
TargetName � ColumnDefinitions ��Name �� VerticalOffset � Value �� ScrollableHeight � Maximum �� ViewportHeight �� ViewportSize  �� Triggers! J� SnapsToDevicePixels" z� SharedSizeGroup# ��Name$ F�Name% �� RelativeSource& �� 	Converter' �Name( � HorizontalOffset) � VerticalOffset* m� IsSubmenuOpen+ � AllowsTransparency, K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation-   DirectionalNavigation��%�  &	        &	      &	 Y     &	 y     &	 �    &	 �    7  '�� n    7  '�� �    7  '�� J	    7  '�� �    7  'o� �    7  'm� �    7  7   '�� �    '�� 9    7  '�� �    '�� l    '��     �� 5   
   6'   *�5   
   2.pack://application:,,,/Fonts/#Fira Code Retina)�6T   *�5   
   Segoe UI)�64   �� 5   
   " ��6)   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      � 	 5      �� 5   "   $
��Auto �60   �� 5   "   $
��Auto �60   #��z��6>   �� 5   "   $
��Auto �60   .+�5      �� $
 TrackBackground��6%   $��1��6>   $��4��5    %   ���#21252b$ 2��5"   %   ]� 5#      $ 
PART_Track��6$   $��1��68    5$   "   �� 5%   &   #��Z 63   ��5&   *   �� 5'   .   " ��6>   .R�5(   2   �� $��4��69   ���	#80FFFFFF$ 2��6[   5)   /   5+   '    5-   "   �� 5.   &   #��Z 63   ��5/   *   �� 50   .   " ��6>   .R�51   2   �� $��4��69   
������ 6D   52   /   54   '    56   "   n� 57   &   ��58   *   �� 59   .   " n�6>   .R�5:   2   A� $��12��6:   $��12��6E   ���#DCDCDC6Q   5;   /   5=   '   5?      5@      5A      5C      5D      �� 5G   
   " n�6-   .�5H      ��  �@�OverridesDefaultStyle$
 true��66   �� 5I       � ��$ false��6*   �� 5J       � �� 5K      �� 5L      " n�6&   .R�5M      �� ���	#50FFFFFF$ 2��5N   "   $ 2��5O   "   5P      5R      5S      �� 5U   
   " ��6(   .�5V      ��  � ��$ 8��6&   �� 5W       � ��
 ���� 6+   �� 5X       � �� 5Y      �� 5Z      " ��6&   .R�5[      � .+�5\      �� $��8��6(   
������ 5]   &   ]� 5^      $ 
PART_Track��6$   $
��true��5_   #    5`   "   n� 5a   &   8��  6,   5c      5d       5e      P� 5f       �@J�IsMouseOver$
 True��6=   . �5g   "   �� $ 
PART_Track��6)    �@J�Opacity$ 1Q�5h   <   5i      P� 5j       �@J�IsMouseOver$ False��6=   . �5k   "   �� $ 
PART_Track��6)    �@J�Opacity$	 0.5Q�5l   <   5m      5o      5q      5r      �� 5t   
   " ��6   .�5u      ��  � �� 5v      �� 5w      " ��6&   .R�5x      �  5y      �� 5z   "   $��* �63   �� 5{   "   $
��Auto �63   .+�5}      �� $��0��65   �� 5~      $ PART_VerticalScrollBar��6(   $��1��5   &   # z 5�   &   # z 5�   &   # z 5�   &   8��  5�   &   5�      5�      5�      5�      �� 5�   
   " ��6   .�5�      ��  � ��8   6+   �� 5�       � �� �#DCDCDC6+   5�      �� 5�   
   " ��6   .�5�      ��  � ��8   6+   �� 5�       � ��$
 10,5q�6(   �� 5�       � �� �#21252b6+   �� 5�       � �� �#DCDCDC6+   �� 5�       � ��$ 0q�60     5�      P� 5�       �@J�IsMouseOver$
 True��61   . �5�      ��  � �� �#2c313a63   5�      P� 5�       � ��$
 True��6/   . �5�      ��  � �� �#1d1f2363   5�      P� 5�       � {�$ False��6/   . �5�      ��  � �� �#1d1f2363   �� 5�       � �� �#70707063   �� 5�       �@J�Opacity$	 0.7Q�60   5�      5�      �� 5�   
   " ��6   .�5�      ��  � ��8   6+   �� 5�       � ��$	 5,3q�6(   �� 5�       � ��$ 1q�60   �� 5�       � �� �#21252b6+   �� 5�       � �� �#DCDCDC6+   �� 5�       �@��
CaretBrush �#DCDCDC6+   �� 5�       � �� �#1d1f236,   5�      �� 5�   
   " o�6   .�5�      ��  � ��8   6+   �� 5�       � ��
 ���� 6+   �� 5�       � �� �#DCDCDC6+   5�      �� 5�   
   " m�6   .�5�      ��  � ��8   6+   �� 5�       � �� �#DCDCDC6+   �� 5�       � �� �#21252b6+   �� 5�       � ��
 ���� 6,   �� 5�       � ��$ 0q�60   �� 5�       � ��$ 5q�6(   �� 5�       � �� 5�      �� 5�      " m�6&   .R�5�      �� $
 Border��6!   #��z��5�       $��0q�5�       $
! True��5�       ._�5�      �  5�   "   �� 5�   &   $
��Auto �67   $
" Icon��6D   �� 5�   &   $��* �67   �� 5�   &   $
��Auto �67   $" Shortcut��6D   �� 5�   &   $��13 �67   .+�5�   "   �� $
# Icon��63   $��6,0,6,0q�6A   $��Center=�6R   $
��Icon��6m   �� 5�   "   $# 
HeaderHost��63   $��1��6G   $��Header��6W   $
��True��6n   $	��2,4q�6�   �� 5�   "   $# SubmenuPresenter��63   $��3��6M   $��10,3,0,3q�6]   $��InputGestureText��6o   F� 5�   "   $$ 	ArrowPath��6'   $��3��6:   $��Right=�6J   $��Center=�6f   "����        � 	= �g5 �    ��j 	���#DCDCDC$��4,0,6,0q�5�   S   y�5�   '   �� *
HasItems+% �� *TemplatedParent+8&  � 5�   "   $' Popup��6(   $��Right=�67   $( -1��5�   (   $) -3��6>   #��z* 5�   (   $
+ True��5�   (   $|�False��5�   (   $
��Fade=�5�   (   .��5�   &   �� $
 SubmenuBorder��6-   ���#21252b���#1d1f23$��1q�5�   ,   $
! True��5�   ,   ._�5�   *   �� $
, True��65   $- Cycle=�5�   3   5�   '   5�   #   5�      5�       5�      P� 5�       �@m�IsHighlighted$
 true��6?   . �5�   "   ��  � �� �#2c313a6?   5�      P� 5�       � {�$ False��6;   . �5�   "   ��  � �� �#7070706?   5�      P� 5�       �@m�Role$ TopLevelHeader=�66   . �5�   "   �� $ Popup��6)    � ��$ Bottom=�6Q   �� 5�   "   $ 	ArrowPath��6)    � y�$ 	Collapsed=�6V   5�      P� 5�       �@m�Role$ TopLevelItem=�66   . �5�   "   �� $ 	ArrowPath��6)    � y�$ 	Collapsed=�6V   5�      P� 5�       �@m�Role$ SubmenuHeader=�66   . �5�   "   �� $ Popup��6)    � ��$ Right=�6Q   5�      5�      5�      5�      �� 5�   
   " ��6   .�5�      ��  � �� �#1d1f236+   �� 5�       � ��$	 0,2q�6'   5�      �� 5  
   " ��6   .�5     ��  � ��8   6+   �� 5      � �� �#DCDCDC6+   5     �� 5  
   " ��6   .�5	     ��  � �� �#21252b6+   �� 5
      � �� �#1d1f236,   �� 5      � ��$ 1q�60   �� 5      � �� �#DCDCDC6+   5     �� 5  
   " ��6   .�5     ��  � ��
 ���� 6+   �� 5      � �� �#DCDCDC6+   �� 5      � ��$ 0q�60   �� 5      � ��$ 5q�6(     5     P� 5      �@��
IsSelected$
 True��60   . �5     ��  � �� �#2c313a63   5     P� 5      �@J�IsMouseOver$
 True��61   . �5     ��  � �� �#2c313a63   5     5     �� 5   
   " ��6   .�5!     ��  � �� �#21252b6+   �� 5"      � �� �#1d1f236,   �� 5#      � ��$ 1q�60   �� 5$      � ��$ 2q�6(   5%     5&     .��5(     �� $��1q�6   ���#1d1f23._�5)  
   ]� .+�5+     � $	��Top=�6   $��32��6(   ���#21252b 5,     �� 5-     $
��Auto �6'   �� 5.     $��* �6'   �� 5/     $
��Auto �6'   .+�53     o� $��0��6   $��Center=�6'   $��5,0,0,0q�6B   .@�54     m� $��_File��6   .@�55     m� -   5        . m�Name$. LoadVideoMenuItem��55  #   $��_Load Video��6>   6S   m� 56     -   5        $. LoadSubtitlesMenuItem��56  #   $��Load _Subtitles��6B   6[   m� 57     -   5        $. SaveSubtitlesMenuItem��57  #   $��_Save Subtitles��6B   6[   �� 58     m� 59     -   5        $��E_xit��59  #   62   5:     m� 5;     $��_Options��6   .@�5<     m� -   5        $��_Preferences��5<  #   69   5=     5>     �� 5A     $��1��6   $��LinguaPlayer��6,   $��Center=�5B     $��Center=�66   �� 5E     $��2��6   / �� Orientation$/ 
Horizontal=�6-   .+�5F     �� -   5        $	��−��5F     $��45��6)   $��32��64   6@   �� 5G     -   5        $	��□��5G     $��45��6)   $��32��64   6@   �� 5H     -   5        $��×��5H     $��45��6)   $��32��64   6@   5I     5J     � 5M     ���#282c34 5N     �� 5O     $	��300 �6'   �� 5P     $��* �6'   	 5R     �� 5S     $��* �6$   �� 5T     $	��100 �6$   .+�5X     � $��0��6   $��0��6'   $��2��64   $��10q�6E   	 5Y     �� 5Z     $
��Auto �6(   �� 5[     $��* �6(   �� 5\     $
��Auto �6(   .+�5`     �� $��Subtitle Browser��6    $
��Bold"�68   $��0,0,0,10q�6J   �� 5c     -	   5        0 ��Name$0 SubtitleListView��5c     $��1��69   $��0,0,0,10q�5d     1 �� SelectionMode$1 Extended=�5e     $��1q�5g     ���#21252b���#1d1f23��5j     �� 5k     .R�5l  "   �� $	��0,5q�6-   .+�5m  &   �� $
��Bold"�6O   ���	#FFDCDCDC��60   �� *FormattedTime+5n  .   �� 5o  &   $
��Wrap=�6F   $��0,3,0,0q�5p  .   #��[ 5q  .   ���	#FFDCDCDC��5o  0   �� *Text+5r  .   5s  #   5t     5v     � 5y     $��2��6   	 5z     �� 5{     $
��Auto �6,   �� 5|     $
��Auto �6,   �� 5}     $
��Auto �6,   �� 5~     $
��Auto �6,   �� 5     $
��Auto �6,   .+�5�     �� $��Subtitle Editor��6$   $
��Bold"�6;   $��0��5�  "   $��0,0,0,10q�6/   � 5�     $��1��6   $��0,0,0,10q�6,    5�     �� 5�  "   $
��Auto �63   �� 5�  "   $��* �63   �� 5�  "   $
��Auto �63   	 5�     �� 5�  "   $
��Auto �60   �� 5�  "   $
��Auto �60   �� 5�  "   $
��Auto �60   .+�5�     �� $��Start:��6(   $��0��66   $��0��6C   $��Center=�5�  &   $��0,0,10,5q�6A   �� 5�     -
   5        2 ��Name$2 StartTimeTextBox��5�  &   $��0��6@   $��1��6M   $	��5,3q�5�  %   $��0,0,10,5q�63   �� 5�     -   5        3 ��Name$3 SyncStartButton��5�  %   $
��Sync��6>   $��0��6M   $��2��5�  %   $
��10,3q�65   ���#21252b5�  %   �� 5�     $
��End:��6(   $��1��64   $��0��6A   $��Center=�5�  &   $��0,0,10,0q�6A   �� 5�     -   5        $2 EndTimeTextBox��5�  &   $��1��6>   $��1��6K   $	��5,3q�5�  %   $��0,0,10,0q�63   �� 5�     -   5        $3 SyncEndButton��5�  %   $
��Sync��6<   $��1��6K   $��2��5�  %   $
��10,3q�65   ���#21252b5�  %   �� 5�     -   5        4 ��Name$4 LockEditorCheckBox��5�  '   $��Lock Editor��6C   $��2��5�  &   $��1��63   $��0,5,0,0q�6C   5�     �� 5�     $��Subtitle Text:��6$   $��2��6:   $��0,0,0,5q�6G   �� 5�     -   5        $2 SubtitleTextBox��5�  "   $��3��6;   5 �� AcceptsReturn$
5 True��5�  "   6 �� TextWrapping$
6 Wrap=�67   $��80��5�  "   "7 �� VerticalScrollBarVisibility$
7 Auto=�5�  "   $��5q�5�  "   $��0,0,0,10q�5�  "   �� 5�     -   5        $3 ApplyChangesButton��5�  !   $��Apply Changes��6=   $��4��5�  !   $��Right=�5�  !   $
��15,5q�5�  !   ���#21252b$��0,0,0,5q�5�  !   5�     5�     � 5�     $��1��6   $��0��6'   $��5q�64   .+�5�     r� -   5        8 r�Name$8 VideoPlayer��5�  #   9 r� LoadedBehavior$9 Manual=�5�  "   : r� UnloadedBehavior$
: Stop=�5�  "   ; r� Stretch$; Uniform=�5�  "   < r� IsMuted$< False��5�  "   = r� Volume$= 1Q�5�  "   �� 5�     $��Bottom=�6   $��0,0,0,20q�5�     ���	#80000000> �� Padding$
> 10,5q�5�     ._�5�     �� -   5        ? ��Name$? SubtitleOverlay��5�  $   $�� ��5�  "   $
��Wrap=�5�  "   @ �� TextAlignment$@ Center=�5�  "   
�������$��24(�5�  "   #��[ 5�  "   $��SemiBold"�5�  "   5�     5�     � 5�     $��1��6   $��1��6'   $��5q�64   	 5�     �� 5�     $
��Auto �6(   �� 5�     $
��Auto �6(   .+�5�     �� -   5        A ��Name$A TimelineSlider��5�     $��0��5�     $��0,0,0,5q�5�     #��[ 5�     B �� IsMoveToPointEnabled$
B True��5�     C �� IsSelectionRangeEnabled$
C True��5�     D � Cursor$
D Hand��5�     $|�False��5�     5�     �� 5�     $��1��6!   $/ 
Horizontal=�6.   $��Center=�6G   .+�5�     �� -   5        $3 PlayPauseButton��5�  !   $
��Play��6:   $��0,0,5,0q�6I   6Z   �� 5�     -   5        $? TimeDisplay��5�  $   $��Center=�69   $��00:00:00 / 00:00:00��5�  $   $	��5,0q�6?   5�     5�     5�     5�     5�     5�     