   M S B A M L   `   `   `  ���� L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 4    .MyDentApplication.ManageReceivedInvoicesWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     1localclr-namespace:MyDentApplication     X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  3System&clr-namespace:System;assembly=mscorlib  -     �Name$  Window�� � Title$ Administrar facturas��$��696.716��$	��480�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � WindowStartupLocation$ CenterScreen=� � 	Resources *   $MyDentApplication.DateValueConverter   DateValueConverter��%   &	          .��� -    �Name$ 
LayoutRoot��.+�&   System.Windows.Controls.DataGrid  -     Name$ 
dgInvoices��$��3��   SelectionMode$ Single=�$��10,84.5,10,46q�   
IsReadOnly . 	   CanUserAddRows	 .  
   CanUserSortColumns
 .    CanUserDeleteRows .     AutoGenerateColumns .  ���� *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	Proveedor��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.15* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *ResourceProvider.Name+  $ Fecha de fact.��$
 0.1*  `�  `� 	Converter# [  .1���  �� Path$ InvoiceDate�  $ Fecha de compra��$ 0.11*  `� # [  .1��� $ PurchaseDate�  $ Folio��$ 0.07*  �� *Folio+  $ Método de pago��$ 0.11*  �� *
PaidMethod+4  .System.Windows.Controls.DataGridCheckBoxColumn  $ Pagada��$ 0.055*  �� *IsPaid+  $ Total fact.��$
 0.1*  �� *TotalAmount+ �� StringFormat$ ${0:n}���� -    ��Name$ btnDeleteInvoice��$��60��$��Bottom=�$��6��$��
140,0,0,10q�$��29��$
��Left=�$��Eliminar�� � Cursor$
 Hand���� -   $ btnEditInvoice��$��60��$��Bottom=�$��5��$��	75,0,0,10q�$��29��$
��Left=�$��Editar��$
 Hand���� -   $ btnAddInvoice��$��60��$��Bottom=�$��4��$��	10,0,0,10q�$��29��$
��Left=�$��Agregar��$
 Hand���� $	��118��$	��Top=�$��13,39.5,0,0q�$��27��$
��Left=�$��Seleccione un mes:��&   Xceed.Wpf.Toolkit.DateTimeUpDown  -     Name$ dtudSelectedMonth��$
��Left=�$
��25.5��$��
131,41,0,0q�$	��Top=�$��187.5��   Format$ 	YearMonth=� K� ZIndex$ 1���� -   $ btnViewInvoices��$��2��$��331.117,21.53,0,0q�$��Ver facturas��$
 Hand��$��29��$	��Top=�$
��Left=�$��94���� -	    ��Name$ lblTotalMonth��$�� ��$��205,0,10,10q�$��Right=�$��29��$��Bottom=��� -
    ��Name$ cbFilter��$��26��$��131,8.5,0,0q�$	��Top=�$��Normal"�$��1��$
��Left=�$��187.5�� �� SelectedIndex$ 0��.@���Fecha de fact.����Fecha de compra���� $��69��$	��Top=�$��
62,7.5,0,0q�$��27��$
��Left=�$��Filtrar por:��