   M S B A M L   `   `   `  ���� L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  8    2MyDentApplication.ManageAmericanExpressPaidsWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    /localclr-namespace:MyDentApplication    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     �Name$  Window�� � Title$( "Administrar pagos American Express��$	��652��$	��496�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � WindowStartupLocation$ CenterScreen=� � 	Resources *   $MyDentApplication.DateValueConverter   DateValueConverter��%   &	          .��� -    �Name$ 
LayoutRoot��.+�&   System.Windows.Controls.DataGrid  -     Name$ dgAmericanExpressPaids��$��10,88,12.917,48.469q�   
IsReadOnly .    CanUserSortColumns . 	   AutoGenerateColumns	 .  
   SelectionMode$
 Single=�   CanUserAddRows .     CanUserDeleteRows .  $��1������ *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Día del pago��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.15* 1  +System.Windows.Controls.DataGridBoundColumn   Binding `�  `� 	Converter# [  .1���  �� Path$ PaidDate�  $ Establecimiento��$ 0.15*  �� *Establishment+  $ Concepto��$
 0.2*  �� *	Concept+  $ Total��$
 0.1*  �� *Total+ �� StringFormat$ ${0:n}���� -    ��Name$ btnDeletePaid��$��Eliminar��$��
140,0,0,10q� � Cursor$
 Hand��$��4��$��29��$��Bottom=�$
��Left=�$��60���� -   $ btnEditPaid��$��Editar��$��	75,0,0,10q�$
 Hand��$��3��$
��Left=�$��60��$��29��$��Bottom=��� -   $ 
btnAddPaid��$��Agregar��$
��Left=�$��	10,0,0,10q�$��60��$
 Hand��$��2��$��29��$��Bottom=��� -    ��Name$ lblTotal��$�� ��$��205,0,10,10q�$��Right=�$��29��$��Bottom=�(  "System.Windows.Controls.DatePicker  -     Name$ dpStartDate��$
��Left=�$��
108,10,0,0q�$	��Top=�$��140.64��  -	   $ 	dpEndDate��$
��Left=�$��
108,40,0,0q�$	��Top=�$��140.64���� $��Fecha de inicio:��$
��Left=�$��24��$��	10,10,0,0q�$	��Top=�$��93���� $��Fecha de fin:��$
��Left=�$��24��$��25.333,39,0,0q�$	��Top=�$��77.667���� -
   $ 
btnRefresh��$��256.64,24,0,0q�$
 Hand��$
��Left=�$��25��$��25��$	��Top=�$��2��.���� $:��4/MyDentApplication;component/Images/refresh_icon.png��$
��Fill=�$��20��