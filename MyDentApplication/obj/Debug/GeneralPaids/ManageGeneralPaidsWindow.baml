   M S B A M L   `   `   `  ����L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  5      0    *MyDentApplication.ManageGeneralPaidsWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      1localclr-namespace:MyDentApplication     5      X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  60   -   5          �Name$  Window��5       � Title$! Administrar pagos generales��5      $��696.716��5      $	��480��6    � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico��6    � WindowStartupLocation$ CenterScreen=�6^    � 	Resources 5	      *   $MyDentApplication.DateValueConverter   DateValueConverter��%   &	          5
      6   5      .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dgGeneralPaids��5      $��3��6%      SelectionMode$ Single=�62   $��10,47.5,10,46q�6I      
IsReadOnly . 	   CanUserAddRows	 .  
   CanUserSortColumns
 .    CanUserDeleteRows .     AutoGenerateColumns .  ��5      �� *ObservableData+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Proveedor��6:      Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.15* 6M   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6   �� *ProviderName+6M     5      $ Fecha de compra��6   $ 0.11* 62    5      `� 5       `� 	Converter# [  6   .1�5   	   ��  �� Path$ PurchaseDate�6   5   	   5        5      $ No. de ticket��6:   $ 0.07* 6Q    6   �� *TicketNumber+6Q     5      $ Método de pago��68   $ 0.11* 6P    6   �� *
PaidMethod+6P     5      $ Total de la compra��6\   $
 0.1* 6x    6%   �� *TotalAmount+ �� StringFormat$ ${0:n}��6x   5      �� 5      -   5         ��Name$ btnDeleteGeneralPaid��5      $��60��6)   $��Bottom=�64   $��6��6O   $��
140,0,0,10q�6\   $��29��6p   $
��Left=�6|   $��Eliminar��6�    � Cursor$
 Hand��6�   6�   �� 5      -   5        $ btnEditGeneralPaid��5      $��60��6'   $��Bottom=�62   $��5��6M   $��	75,0,0,10q�6Z   $��29��6m   $
��Left=�6y   $��Editar��6�   $
 Hand��6�   6�   �� 5       -   5        $ btnAddGeneralPaid��5       $��60��6&   $��Bottom=�61   $��4��6L   $��	10,0,0,10q�6Y   $��29��6l   $
��Left=�6x   $��Agregar��6�   $
 Hand��6�   6�   �� 5!      -   5         ��Name$ lblTotalMonth��5!   
   $�� ��6!   $��205,0,10,10q�6,   $��Right=�6A   $��29��6d   $��Bottom=�6p   �� 5"      $	��118��6
   $	��Top=�6   $��	10,10,0,0q�6.   $��27��6A   $
��Left=�6M   $��Seleccione un mes:��6h   &   Xceed.Wpf.Toolkit.DateTimeUpDown  5#      -   5          Name$ dtudSelectedMonth��5#      $
��Left=�63   $
��25.5��6N   $��133,11.5,0,0q�6\   $	��Top=�6r   $��187.5��6�      Format$ 	YearMonth=�6�    K� ZIndex$ 1��6�   �� 5$      -	   5        $ btnRefreshGeneralPaids��5$      $��325.5,12,0,0q�6+   $
 Hand��6A   $
��Left=�6O   $��25��6j   $��25��6u   $	��Top=�6�   .��5%      �� $:��4/MyDentApplication;component/Images/refresh_icon.png��6   $
��Fill=�6I   $��20��6X   5&      5'      5(      