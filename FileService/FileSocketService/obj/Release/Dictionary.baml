   M S B A M L   `   `   `  ���� B  >Service, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ServiceService  O KRRQMSkin, Version=1.0.8.0, Culture=neutral, PublicKeyToken=8f999a2ed33dcd585(clr-namespace:RRQMSkin;assembly=RRQMSkinRRQMSkin W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   SpeedDialStyle  �� 
TargetType   RRQMSkin.SpeedDial �� Property �� Value �� 
TargetType  RRQMSkin.Sector   EndAngle   MaxRadiusRatio   MinRadiusRatio   
StartAngle    PointerAngle  RRQMSkin.Dial	   MaxRadiusRatio
   MinRadiusRatio   Stroke   StrokeThickness   
StartAngle   	TickCount   	TickAngle  RRQMSkin.DialText   RadiusRatio   
StartAngle   Text   	TickAngle �� StringFormat  RRQMSkin.Pointer  Name   
RatioAngle   RatioCenter J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   
BlurRadius   Color   ShadowDepth �� EndPoint �� 
StartPoint � Color � Offset  ScrollBarThumbVertical  ��Name! �� CornerRadius" J� SnapsToDevicePixels# �� Triggers$ P� Property% P� Value& �� 
TargetName' �Name( � RowDefinitions) ]�Name* J� IsMouseOver+ ]� Thumb, � ColumnDefinitions- ��Name. �� CanContentScroll/ �� CanHorizontallyScroll0 �� CanVerticallyScroll1 ��Name4  .System.Windows.Automation.AutomationProperties2   AutomationId3 � Cursor4 �� ScrollableHeight5 � Maximum6 � Minimum*7 �� #ComputedVerticalScrollBarVisibility8 �� ViewportHeight9 �� ViewportSize: � Value; �� Mode< �� RelativeSource�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     localclr-namespace:Service  5RRQMSkin(clr-namespace:RRQMSkin;assembly=RRQMSkin  %�  &	        &	 %    '�� �    7  '��     �� "    .���  � �� �� "   .R�� $	��200��$	��200��.+�A�   $	 230Q����	#FF00BE8B$
 0.98Q�$	 0.8Q�$	 149Q�  $	 320Q����	#FF14BEFF$
 0.98Q�$	 0.8Q�$	 230Q�  $	 391Q����	#FFFF2975$
 0.98Q�$	 0.8Q�$	 320Q�  # z 
��� ���$	 0.8Q�$	 0.7Q�$	 149Q�  $
	 0.95Q�$	
 0.7Q�
 �   �$ 3Q�$	 150Q�$ 9��$ 30Q�  $
	 0.95Q�$	
 0.8Q�
 �   �$ 2Q�$	 165Q�$ 8��$ 30Q�  $
	 0.95Q�$	
 0.8Q�
 �   �$ 1Q�$ 157.5Q�$ 16��$ 15Q�  $��17(�$	 0.6Q�$	 150Q�$$ 0,30,60,90,120,150,180,210,240��$ 30Q�  $��10(�$	 0.7Q�$	 165Q�$" 15,45,75,105,135,165,195,225��$ 30Q�  $��20(�$	 0.5Q�$ 80Q�$ 单位（MB）��  $��12(�$	 0.3Q�$ 80Q� �� *Speed+$ 0.00MB/s��  $ pointer��# z $ 0.2,0.5&�   $ 10Q�$
 Blue��$ 0Q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF3C57F5��$ 0Q�� $ 	#FFF38F8A��$ 1Q�A� 
�������$��10��$��10��A� $��8��
����� �$��1��$��8���� "  n�.���  �@�OverridesDefaultStyle . ��  � �� .  ��  � �� �� " n�.R��� $  	rectangle��$��8��$! 4��#��z��" . # P� $ �@J�IsMouseOver% . . ���  �@J�Opacity$ 1Q��� $& 	rectangle�� � �� �#A6A6A6P� $ �@J�IsMouseOver% .  . ���  �@J�Opacity$	 0.2Q��� $& 	rectangle�� � �� �#A6A6A6P� $ �@n�
IsDragging% . . ��� $& 	rectangle�� � �� �#606060�� "  ��.���  �@��IsPressAndHoldEnabled .  ��  �@��IsFlicksEnabled .  ��  � ��
 ���� ��  � ��$ 2����  � �� �� " ��.R�� $' Bg��" . ( �� $��0.00001* �.+��� $��8��$��0q�
������ ]� $) 
PART_Track����. #{�z* + n� 8��  �� "  ��.���  � �� �� " ��.R�� $
' Grid��
������ , �� $��* ��� $
��Auto �( �� $��* ��� $
��Auto �.+��� $!- PART_ScrollContentPresenter��#. z��/ .  0 .  #��z��#��z��$��0��$��2��#��z��$��0���� $1 PART_VerticalScrollBar��$2 VerticalScrollBar��$3 Arrow��$��1��#5 z4 $6 0Q�$��0��#y�z7 #9 z8 : �� *VerticalOffset+$; OneWay=�< �� *TemplatedParent+