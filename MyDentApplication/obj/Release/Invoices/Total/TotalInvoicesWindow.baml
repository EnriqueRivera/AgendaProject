   M S B A M L   `   `   `  ���� L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  +    %MyDentApplication.TotalInvoicesWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     1localclr-namespace:MyDentApplication     X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -     �Name$  Window�� � Title$ Facturacion mensual��$��696.716��$	��480�� � WindowStartupLocation$ CenterScreen=� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � 	Resources *   $MyDentApplication.DateValueConverter   DateValueConverter��%   &	          .��� -    �Name$ 
LayoutRoot�� � RowDefinitions �� $
��113* ��� $
��112* �.+�&   System.Windows.Controls.DataGrid  -     Name$ dgReceivedInvoices��$��3��   SelectionMode$ Single=�$��10,31.5,10,44.5q�	   
IsReadOnly	 . 
   CanUserAddRows
 .     CanUserSortColumns .    CanUserDeleteRows .     AutoGenerateColumns .  $��1������ *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	Proveedor��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.15* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *ResourceProvider.Name+  $ Fecha de fact.��$
 0.1*  `�  `� 	Converter# [  .1���  �� Path$ InvoiceDate�  $ Fecha de compra��$ 0.11*  `� # [  .1��� $ PurchaseDate�  $ Folio��$ 0.07*  �� *Folio+  $ Método de pago��$ 0.11*  �� *
PaidMethod+4  .System.Windows.Controls.DataGridCheckBoxColumn  $ Pagada��$ 0.055*  �� *IsPaid+  $ Total fact.��$
 0.1*  �� *TotalAmount+ �� StringFormat$ ${0:n}���� $	��118��$	��Top=�$��	10,10,0,0q�$��27��$
��Left=�$��Seleccione un mes:��&   Xceed.Wpf.Toolkit.DateTimeUpDown  -     Name$ dtudSelectedMonth��$
��Left=�$
��25.5��$��133,11.5,0,0q�$	��Top=�$��187.5��   Format$ 	YearMonth=� K� ZIndex$ 1��$��1���� -    ��Name$ lblTotalMonth��$�� ��$��114.333,0,10,10q�$��Right=�$��29��$��Bottom=�$��1���� $	��218��$	��Top=�$��10,44.5,0,0q�$��27��$
��Left=�$��Recibos de honorarios:���� $	��118��$	��Top=�$��
10,4.5,0,0q�$��27��$
��Left=�$��	Facturas:��$��1��  -   $ dgOutgoingInvoices��$��4��$ Single=�$��10,71.5,10,6q�	 . 
 .   .  .   .  ���� *ObservableData+   $ Paciente��$ 0.15*  `� $ (Exp. No. {0}) {1} {2}��.1��� $ Patient.AssignedId��� $ Patient.FirstName��� $ Patient.LastName�  $ Fecha de fact.��$
 0.1*  `� # [  .1��� $ InvoiceDate�  $ Fecha del pago��$ 0.11*  `� # [  .1��� $ PaidDate�  $ Folio��$ 0.07*  �� *Folio+  $ Método de pago��$ 0.11*  �� *
PaidMethod+  $ Total fact.��$
 0.1*  �� *TotalAmount+$ ${0:n}���� -    ��Name$ btnRefreshInvoices��$��325.5,12,0,0q� � Cursor$
 Hand��$
��Left=�$��25��$��25��$	��Top=�$��2��.���� $:��4/MyDentApplication;component/Images/refresh_icon.png��$
��Fill=�$��20���� -   $ btnExportToPdf��$��Exportar a PDF��$��	10,0,0,10q�$
 Hand��$��3��$��29��$��Bottom=�$��1��$
��Left=�$��99.333��