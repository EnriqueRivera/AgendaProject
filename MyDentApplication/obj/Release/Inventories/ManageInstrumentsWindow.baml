   M S B A M L   `   `   `  ���� L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem /    )MyDentApplication.ManageInstrumentsWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     1localclr-namespace:MyDentApplication     X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  3System&clr-namespace:System;assembly=mscorlib  -     �Name$  Window�� � Title$  Inventario de instrumental��$��782.716��$	��480�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ 
LayoutRoot��.+�&   System.Windows.Controls.DataGrid  -     Name$ dgInstruments��$��3��   SelectionMode$ Single=�$��10,46.5,10,103q�   
IsReadOnly .    CanUserAddRows .  	   CanUserSortColumns	 . 
   CanUserDeleteRows
 .     AutoGenerateColumns .  ���� *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Instrumento��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.07* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *InstrumentName+  $ Cantidad actual��$ 0.07*  �� *
Quantity+  $ Usado en (Tratamientos)��$ 0.07*  �� *UsedOn+  $ Usos restantes��$ 0.07*  �� *
UsesLeft+  $ 
Comentario��$ 0.07*  �� *	Comment+4  .System.Windows.Controls.DataGridCheckBoxColumn  $ Revisado��$ 0.03*  �� *	IsChecked+�� -    ��Name$ btnDeleteInstrument��$	��130��$��Bottom=�$��6��$��	0,0,10,69q�$��29��$��Right=�$��Eliminar instrumento�� � Cursor$
 Hand���� -   $ btnEditInstrument��$	��119��$��Bottom=�$��5��$��
0,0,145,69q�$��29��$��Right=�$��Editar instrumento��$
 Hand���� -   $ btnAddInstrument��$	��144��$��Bottom=�$��4��$��
0,0,269,69q�$��29��$��Right=�$��Agregar instrumento��$
 Hand���� $	��118��$	��Top=�$��
10,8.5,0,0q�$��27��$
��Left=�$��Seleccione un mes:��&   Xceed.Wpf.Toolkit.DateTimeUpDown  -     Name$ dtudSelectedMonth��$
��Left=�$
��25.5��$��
133,10,0,0q�$	��Top=�$��187.5��   Format$ 	YearMonth=� K� ZIndex$ 1���� -   $ 
btnRefresh��$��325.5,10,0,0q�$
 Hand��$
��Left=�$��25��$��25��$	��Top=�.���� $:��4/MyDentApplication;component/Images/refresh_icon.png��$
��Fill=�$��20���� -	   $ btnSignature��$��158.216��$��Bottom=�$��4��$��	10,0,0,69q�$��29��$
��Left=�$��Firmar inventario del mes��$
 Hand���� $��113.5��$	��Top=�$��
396,10,0,0q�$��27��$
��Left=�$��Fecha seleccionada:���� -
    ��Name$ lblSelectedMonth��$	��Top=�$��514.5,10,10,0q�$��27��$�� ���� -   $ btnAddEditRevision��$��182.5��$��Bottom=�$��5��$��0,0,10,27.47q�$��29��$��Right=�$��Agregar/Editar revisión��$
 Hand���� $
��53.5��$��Bottom=�$��	10,0,0,37q�$��27��$
��Left=�$��Firma 1:���� $
��53.5��$��	10,0,0,10q�$
��Left=�$��Firma 2:��$��27��$��Bottom=��� -   $ lblSignature��$��Bottom=�$��68.5,0,424.216,37q�$��27��$�� ���� -   $ lblSignature1��$��68.5,0,424.216,10q�$�� ��$��27��$��Bottom=��� -   $ btnViewTreatments��$��104.216��$��Bottom=�$��4��$��
0,0,418,69q�$��29��$��Right=�$��Ver tratamientos��$
 Hand��