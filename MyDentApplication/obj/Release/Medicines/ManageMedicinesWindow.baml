   M S B A M L   `   `   `  ���� L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  -    'MyDentApplication.ManageMedicinesWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     1localclr-namespace:MyDentApplication     X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -     �Name$  Window�� � Title$ Administrar medicamentos��$��696.716��$	��480�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � WindowStartupLocation$ CenterScreen=� � 	Resources *   $MyDentApplication.DateValueConverter   MonthYearValueConverter��%   &	          .��� -    �Name$ 
LayoutRoot��.+�&   System.Windows.Controls.DataGrid  -     Name$ dgMedicines��$��10,52,10,46q�   
IsReadOnly .    CanUserSortColumns . 	   AutoGenerateColumns	 .  
   SelectionMode$
 Single=�   CanUserAddRows .     CanUserDeleteRows .  $��1������ *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Producto��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.15* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Name+  $ Marca��$
 0.1*  �� *Brand+  $ Fecha de caducidad��$ 0.19*  `�  `� 	Converter# [  .1���  �� Path$ ExpiredDate�  $
 Lote��$ 0.08*  �� *Batch+  $ 	Asistente��$ 0.15*  `�  �� StringFormat$ {0} {1}��.1��� $ User.FirstName��� $ User.LastName�  $ Observaciones��$
 0.2*  �� *Notes+4  .System.Windows.Controls.DataGridCheckBoxColumn  $ Reemplazado��$ 0.13*  �� *WasReplaced+�� -    ��Name$ btnDeleteMedicine��$��Eliminar��$��	0,0,10,10q� � Cursor$
 Hand��$��5��$��29��$��Bottom=�$��Right=�$��60���� -   $ btnEditMedicine��$��Editar��$��	0,0,75,10q�$
 Hand��$��4��$��Right=�$��60��$��29��$��Bottom=��� -   $ btnAddMedicine��$��Agregar��$��Right=�$��
0,0,140,10q�$��60��$
 Hand��$��3��$��29��$��Bottom=��� $	��118��$	��Top=�$��13,12.5,0,0q�$��27��$
��Left=�$��Seleccione un mes:��&   Xceed.Wpf.Toolkit.DateTimeUpDown  -     Name$ dtudMedicines��$
��Left=�$
��25.5��$��
131,14,0,0q�$	��Top=�$��187.5��   Format$ 	YearMonth=� K� ZIndex$ 1���� -   $ btnRefreshMedicines��$��323.5,14.5,0,0q�$
��Left=�$��25��$
 Hand��$��25��$	��Top=�.���� $:��4/MyDentApplication;component/Images/refresh_icon.png��$
��Fill=�$��20���� -	   $ btnViewAllMedicines��$��Ver todo��$��25��$��0,14.5,10,0q�$	��Top=�$��Right=�$��63.333��$
 Hand��