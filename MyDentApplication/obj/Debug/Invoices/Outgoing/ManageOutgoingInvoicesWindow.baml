   M S B A M L   `   `   `  ����L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 6g   4    .MyDentApplication.ManageOutgoingInvoicesWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      1localclr-namespace:MyDentApplication     5      X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  60   3System&clr-namespace:System;assembly=mscorlib  6g   -   5          �Name$  Window��5       � Title$' !Administrar recibos de honorarios��5      $��696.716��5      $	��480��6    � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico��6    � WindowStartupLocation$ CenterScreen=�6^    � 	Resources 5	      *   $MyDentApplication.DateValueConverter   DateValueConverter��%   &	          5
      6   5      .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ 
dgInvoices��5      $��3��6!      SelectionMode$ Single=�6.   $��10,84.5,10,46q�6E      
IsReadOnly . 	   CanUserAddRows	 .  
   CanUserSortColumns
 .    CanUserDeleteRows .     AutoGenerateColumns .  ��5      �� *ObservableData+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Paciente��6      Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.15* 6+   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5      `� 5       �� StringFormat$ (Exp. No. {0}) {1} {2}��6   .1�5      ��  �� Path$ Patient.AssignedId�6&   �� 5   	   $ Patient.FirstName�6   �� 5   	   $ Patient.LastName�6   5   	   5        5      $ Fecha de fact.��6   $
 0.1* 61    5      `� 5       `� 	Converter# [  6   .1�5   	   �� $ InvoiceDate�6   5   	   5         5!      $ Fecha del pago��6   $ 0.11* 61    5"      `� 5#      # [  6   .1�5$   	   �� $ PaidDate�6   5%   	   5'        5(      $ Folio��63   $ 0.07* 6B    6   �� *Folio+6B     5)      $ Método de pago��68   $ 0.11* 6P    6   �� *
PaidMethod+6P     5*      $ Total fact.��6\   $
 0.1* 6q    6%   �� *TotalAmount+$ ${0:n}��6q   5,      �� 5-      -   5         ��Name$ btnDeleteInvoice��5-      $��60��6%   $��Bottom=�60   $��6��6K   $��
140,0,0,10q�6X   $��29��6l   $
��Left=�6x   $��Eliminar��6�    � Cursor$
 Hand��6�   6�   �� 5.      -   5        $ btnEditInvoice��5.      $��60��6#   $��Bottom=�6.   $��5��6I   $��	75,0,0,10q�6V   $��29��6i   $
��Left=�6u   $��Editar��6�   $
 Hand��6�   6�   �� 5/      -   5        $ btnAddInvoice��5/      $��60��6"   $��Bottom=�6-   $��4��6H   $��	10,0,0,10q�6U   $��29��6h   $
��Left=�6t   $��Agregar��6�   $
 Hand��6�   6�   �� 50      $	��118��6
   $	��Top=�6   $��13,39.5,0,0q�6.   $��27��6C   $
��Left=�6O   $��Seleccione un mes:��6j   &   Xceed.Wpf.Toolkit.DateTimeUpDown  51      -   5          Name$ dtudSelectedMonth��51      $
��Left=�63   $
��25.5��6N   $��
131,41,0,0q�6\   $	��Top=�6p   $��187.5��6�      Format$ 	YearMonth=�6�    K� ZIndex$ 1��6�   �� 52      -   5        $ btnViewInvoices��52      $��2��6$   $��331.117,21.53,0,0q�61   $��Ver facturas��6L   $
 Hand��6c   $��29��6�   $	��Top=�6�   $
��Left=�6�   $��94��6�   �� 53      -	   5         ��Name$ lblTotalMonth��53   
   $�� ��6!   $��205,0,10,10q�6,   $��Right=�6A   $��29��6d   $��Bottom=�6p   �� 54      -
   5         ��Name$ cbFilter��54      $��26��6   $��131,8.5,0,0q�6+   $	��Top=�6@   $��Normal"�6X   $��1��6l   $
��Left=�6y   $��187.5��6�    �� SelectedIndex$ 0��6�   .@�55      ��Fecha de fact.��6#   ��56      Fecha del pago��6#   57      �� 58      $��69��6
   $	��Top=�6   $��
62,7.5,0,0q�6-   $��27��6A   $
��Left=�6M   $��Filtrar por:��6h   59      5:      