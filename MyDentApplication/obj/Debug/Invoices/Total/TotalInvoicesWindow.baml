   M S B A M L   `   `   `  ����L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  5      +    %MyDentApplication.TotalInvoicesWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      1localclr-namespace:MyDentApplication     5      X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  60   -   5          �Name$  Window��5       � Title$ Facturacion mensual��5      $��696.716��5      $	��480��6    � WindowStartupLocation$ CenterScreen=�6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6D    � 	Resources 5
      *   $MyDentApplication.DateValueConverter   DateValueConverter��%   &	          5      6   5      .��5      � -   5         �Name$ 
LayoutRoot��5       � RowDefinitions 5      �� 5      $
��113* �6   �� 5      $
��112* �6   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dgReceivedInvoices��5      $��3��6)      SelectionMode$ Single=�66   $��10,31.5,10,44.5q�6M   	   
IsReadOnly	 . 
   CanUserAddRows
 .     CanUserSortColumns .    CanUserDeleteRows .     AutoGenerateColumns .  $��1��5   T   ��5      �� *ObservableData+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Proveedor��6C      Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.15* 6V   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6   �� *ResourceProvider.Name+6V     5      $ Fecha de fact.��6   $
 0.1* 61    5      `� 5       `� 	Converter# [  6   .1�5   	   ��  �� Path$ InvoiceDate�6   5   	   5        5      $ Fecha de compra��6   $ 0.11* 62    5       `� 5!      # [  6   .1�5"   	   �� $ PurchaseDate�6   5#   	   5%        5&      $ Folio��63   $ 0.07* 6B    6   �� *Folio+6B     5'      $ Método de pago��68   $ 0.11* 6P    6   �� *
PaidMethod+6P   4  .System.Windows.Controls.DataGridCheckBoxColumn  5(      $ Pagada��68   $ 0.055* 6H    6   �� *IsPaid+6H     5)      $ Total fact.��6\   $
 0.1* 6q    6%   �� *TotalAmount+ �� StringFormat$ ${0:n}��6q   5+      �� 5,      $	��118��6
   $	��Top=�6   $��	10,10,0,0q�6.   $��27��6A   $
��Left=�6M   $��Seleccione un mes:��6h   &   Xceed.Wpf.Toolkit.DateTimeUpDown  5-      -   5          Name$ dtudSelectedMonth��5-      $
��Left=�63   $
��25.5��6N   $��133,11.5,0,0q�6\   $	��Top=�6r   $��187.5��6�      Format$ 	YearMonth=�6�    K� ZIndex$ 1��6�   $��1��6�   �� 5.      -   5         ��Name$ lblTotalMonth��5.   
   $�� ��6!   $��114.333,0,10,10q�6,   $��Right=�6E   $��29��6h   $��Bottom=�6t   $��1��6�   �� 5/      $	��218��6
   $	��Top=�6   $��10,44.5,0,0q�6.   $��27��6C   $
��Left=�6O   $��Recibos de honorarios:��6j   �� 50      $	��118��6
   $	��Top=�6   $��
10,4.5,0,0q�6.   $��27��6B   $
��Left=�6N   $��	Facturas:��6i   $��1��6}     51      -   5        $ dgOutgoingInvoices��51      $��4��6)   $ Single=�66   $��10,71.5,10,6q�6M   	 . 
 .   .  .   .  ��52      �� *ObservableData+ 54        55      $ Paciente��6   $ 0.15* 6+    56      `� 57      $ (Exp. No. {0}) {1} {2}��6   .1�58      �� $ Patient.AssignedId�6&   �� 59   	   $ Patient.FirstName�6   �� 5:   	   $ Patient.LastName�6   5;   	   5=        5>      $ Fecha de fact.��6   $
 0.1* 61    5?      `� 5@      # [  6   .1�5A   	   �� $ InvoiceDate�6   5B   	   5D        5E      $ Fecha del pago��6   $ 0.11* 61    5F      `� 5G      # [  6   .1�5H   	   �� $ PaidDate�6   5I   	   5K        5L      $ Folio��63   $ 0.07* 6B    6   �� *Folio+6B     5M      $ Método de pago��68   $ 0.11* 6P    6   �� *
PaidMethod+6P     5N      $ Total fact.��6\   $
 0.1* 6q    6%   �� *TotalAmount+$ ${0:n}��6q   5P      �� 5Q      -   5         ��Name$ btnRefreshInvoices��5Q      $��325.5,12,0,0q�6'    � Cursor$
 Hand��6^   $
��Left=�6l   $��25��6�   $��25��6�   $	��Top=�6�   $��2��6�   .��5R      �� $:��4/MyDentApplication;component/Images/refresh_icon.png��6   $
��Fill=�6I   $��20��6X   5S      �� 5T      -   5        $ btnExportToPdf��5T      $��Exportar a PDF��6#   $��	10,0,0,10q�6<   $
 Hand��6O   $��3��6]   $��29��6j   $��Bottom=�6v   $��1��6�   $
��Left=�6�   $��99.333��6�   5U      5V      