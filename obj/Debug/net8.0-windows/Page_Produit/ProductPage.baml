   M S B A M L   `   `   `  ����G  CApp_Grosiste, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:App_GrosisteApp_Grosiste  5          App_Grosiste.ProductPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:App_Grosiste     5        N� Title$  ProductPage��5	       N� 
Background
 �����.��5
      �  � ColumnDefinitions 5   
   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��LISTE PRODUITS��6   $��Center=�6)   $��0��6F   $	��Top=�6S   $��61��6k   $	��436��6w   $��Eras ITC)�6�   $��48(�6�   $��Center=�6�   $
��Bold"�6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   �� 5   
   -   5        $��ProductList��5      $��1��6&   $��20,20,20,28q�63   ��5      �� 5       �� Color$ 	#FFBC90EB��6"    �� Opacity$	 0.4Q�64    �� View 5      �� 5      .P�5      �� $��ID��6%    �� Width$	 100��61   	 �� DisplayMemberBinding	 6=   �� *id+�� 5      $	��Nom��6%   $	 300��62   	 6>   �� *nom+�� 5      $��
Quantitée��6%   $	 100��68   	 6D   �� *
quantite+�� 5      $
��Prix��6%   $	 100��63   	 6?   �� *prix+�� 5      $��
Catégorie��6%   $	 250��68   	 6D   �� *categorieNom+�� 5       $��Emplacement��6%   $	 150��6:   	 6F   �� *emplacement+5!      5#      5$      5%      