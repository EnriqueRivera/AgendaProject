   M S B A M L   `   `   `  ����L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 60   /    )MyDentApplication.FindTreatmentPriceModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      1localclr-namespace:MyDentApplication     5      9System&clr-namespace:System;assembly=mscorlib     60   -   5          �Name$  Window��5       � Title$ Buscar tratamiento��5      $	��652��5      $��437.791��6    � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico��6    � WindowStartupLocation$ CenterScreen=�6^   .��5	      � -   5         �Name$ 
LayoutRoot��5	      .+�5
      �� -   5         ��Name$ 	btnAccept��5
      $��Aceptar��6   $��	0,0,75,10q�60    � Cursor$
 Hand��6C   $��5��6Q   $��29��6^   $��Bottom=�6j   $��Right=�6�   $��60��6�   6�   �� 5      -   5        $ 	btnCancel��5      $��Cancelar��6   $��	0,0,10,10q�61   $
 Hand��6D   $��6��6R   $��29��6_   $��Bottom=�6k   $��Right=�6�   $��60��6�   6�   �� 5      -   5        $ btnViewAllTreatmentPrices��5      $��63.333��6.   $	��Top=�6=   $��0,14.5,10,0q�6U   $��25��6j   $��Right=�6v   $��Ver todo��6�   $
 Hand��6�   $��3��6�   �� 5      -   5         ��Name$ cbFilter��5      $��26��6   $��84,16.02,0,0q�6+   $	��Top=�6A   $��Normal"�6Y   $��1��6m   $
��Left=�6z   $	��156��6�    �� SelectedIndex$ 0��6�   .@�5      ��	Clave��6   ��5      Nombre del tratamiento��6+   ��5      	Área��6   5      �� 5      $��69��6
   $	��Top=�6   $��10,14.02,0,0q�6-   $��27��6C   $
��Left=�6O   $��Buscar por:��6j   �� 5      -   5        	 ��Name$	 txtSearchTerm��5      $
��Left=�6#   $��25.02��6>   $��
245,16,0,0q�6M   
 �� TextWrapping$

 Wrap=�6a   $	��Top=�6u   $	��166��6�   
���   �$��1��6�   �� 5      -   5        $ btnRefreshTreatments��5      $��
416,16,0,0q�6)   $
 Hand��6=   $
��Left=�6K   $��25��6f   $��25��6q   $	��Top=�6}   $��2��6�   .��5      �� $:��4/MyDentApplication;component/Images/refresh_icon.png��6   $
��Fill=�6I   $��20��6X   5      &   System.Windows.Controls.DataGrid  5      -	   5          Name$ dgTreatmentPrices��5      $��10,50.353,10,44q�6(      
IsReadOnly .    CanUserSortColumns .    AutoGenerateColumns .     SelectionMode$ Single=�5         CanUserAddRows .     CanUserDeleteRows .  $��1��5      ��5      �� *ObservableData+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Clave��6      Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.15* 6I   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6(   �� *TreatmentKey+6I     5      $ Nombre del tratamiento��6   $
 0.6* 6R    69   �� *Name+6R     5      $ Área��6   $
 0.3* 6@    6'   �� *Type+6@     5      $ Precio��6   $
 0.2* 6Z    6)   �� *Price+ �� StringFormat$ ${0:n}��6Z     5       $ Descuento (%)��6   $ 0.22* 6M    60   �� *
Discount+6M   5"      5#      5$      