   M S B A M L   `   `   `  ����e  aMicrosoft.Web.WebView2.Wpf, Version=1.0.2903.40, Culture=neutral, PublicKeyToken=2a8ab48044d2601ekLclr-namespace:Microsoft.Web.WebView2.Wpf;assembly=Microsoft.Web.WebView2.WpfMicrosoft.Web.WebView2.Wpf  5      G CApp_Grosiste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:App_GrosisteApp_Grosiste 5         App_Grosiste.HomePage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      \wv2Lclr-namespace:Microsoft.Web.WebView2.Wpf;assembly=Microsoft.Web.WebView2.Wpf     5      ,localclr-namespace:App_Grosiste     5        N� Title$  HomePage��5
       N� 
Background
 �����$	��500��5      $	��800��6   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��ACCUEIL��6   $��Center=�5      $	��Top=�5      $��Eras ITC)�5      $��SemiBold"�5      $
��Auto��5      $��	0,10,0,10q�5      $��48(�5      � 5!   
   $��1��6   $��10q�6    � ColumnDefinitions 5"      �� 5#      $	��37* �6#   �� 5$      $	��23* �6#    5&      �� 5'      $
��Auto �6    $��91.54��6.   �� 5)      .+�5.      �� 
�������$��2q�50       �� CornerRadius$ 15��51       �� Padding$ 10q�52      $��10,-22,10,72q�53      $��2��6+   ��54      �� 55       �� Color$ 	#FFBC90EB��6&    �� Opacity$
 0.75Q�68   ._�57      �� -   5        $��
BestSeller��57      $��BEST SELLER��58      $
��Left=�59      $	��Top=�5:      $
��Wrap=�5;      $��Eras ITC)�5<      $��18(�5=      $��	-2,-2,0,0q�6*   5?      �� 5B      -   5        $��TotalProduct��5B      $��TOTAL��5C      $
��Left=�5D      $��412,-12,0,0q�5E      $	��Top=�5F      $��Eras ITC)�5G      $��18(�5H      $��21��6&   $	��345��62   $��2��6>   )   #Microsoft.Web.WebView2.Wpf.WebView2  5L      -   5        $��webView2��5L      $��10,63,10,10q�5M      $	��300��5N      $��2��6+   $��2��6<   �� 5P      $
��Auto=�6   $��Disabled=�6>   $��2��6g   $��	0,0,23,35q�6{   .��5Q      �� -   5        $��Notifcation��5Q      $
��Left=�5R      $��	10,26,0,0q�5S      $	��Top=�5T      $��Eras ITC)�5U      $��18(�5V      $��21��6   $	��760��6+   $��2��67   ���	#FF9E4FF2�� 6c   $�NOTIFICATION: ��6g   $
��Bold"�6}   �� 6�   $� ��6�   �� 6�   -   5         ��Name$ NotificationContent��5V   �   $� ��6�   6�   5W      	 � Triggers	 5X      :� 5Y      
 :� RoutedEvent$
 Loaded��6   .[�5Z      �� .��5[      ��  g� RepeatBehavior$ Forever��6%   .��5\      W�  �� 
TargetName$ Notifcation��6.    �� TargetProperty$8 2(UIElement.RenderTransform).(TranslateTransform.Y)�5]   &    W� From 05^   &   	 W� To -1006/    g� Duration$ 0:0:5D�69    g� AutoReverse$
 True��6J   5_      5`      5a      5c      5d      5e      