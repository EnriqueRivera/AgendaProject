   M S B A M L   `   `   `  ���� L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  0    *MyDentApplication.ManageGeneralPaidsWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     1localclr-namespace:MyDentApplication     X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -     �Name$  Window�� � Title$! Administrar pagos generales��$��696.716��$	��480�� � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico�� � WindowStartupLocation$ CenterScreen=� � 	Resources *   $MyDentApplication.DateValueConverter   DateValueConverter��%   &	          .��� -    �Name$ 
LayoutRoot��.+�&   System.Windows.Controls.DataGrid  -     Name$ dgGeneralPaids��$��3��   SelectionMode$ Single=�$��10,47.5,10,46q�   
IsReadOnly . 	   CanUserAddRows	 .  
   CanUserSortColumns
 .    CanUserDeleteRows .     AutoGenerateColumns .  ���� *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	Proveedor��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.15* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *ProviderName+  $ Fecha de compra��$ 0.11*  `�  `� 	Converter# [  .1���  �� Path$ PurchaseDate�  $ No. de ticket��$ 0.07*  �� *TicketNumber+  $ Método de pago��$ 0.11*  �� *
PaidMethod+  $ Total de la compra��$
 0.1*  �� *TotalAmount+ �� StringFormat$ ${0:n}���� -    ��Name$ btnDeleteGeneralPaid��$��60��$��Bottom=�$��6��$��
140,0,0,10q�$��29��$
��Left=�$��Eliminar�� � Cursor$
 Hand���� -   $ btnEditGeneralPaid��$��60��$��Bottom=�$��5��$��	75,0,0,10q�$��29��$
��Left=�$��Editar��$
 Hand���� -   $ btnAddGeneralPaid��$��60��$��Bottom=�$��4��$��	10,0,0,10q�$��29��$
��Left=�$��Agregar��$
 Hand���� -    ��Name$ lblTotalMonth��$�� ��$��205,0,10,10q�$��Right=�$��29��$��Bottom=��� $	��118��$	��Top=�$��	10,10,0,0q�$��27��$
��Left=�$��Seleccione un mes:��&   Xceed.Wpf.Toolkit.DateTimeUpDown  -     Name$ dtudSelectedMonth��$
��Left=�$
��25.5��$��133,11.5,0,0q�$	��Top=�$��187.5��   Format$ 	YearMonth=� K� ZIndex$ 1���� -	   $ btnRefreshGeneralPaids��$��325.5,12,0,0q�$
 Hand��$
��Left=�$��25��$��25��$	��Top=�.���� $:��4/MyDentApplication;component/Images/refresh_icon.png��$
��Fill=�$��20��