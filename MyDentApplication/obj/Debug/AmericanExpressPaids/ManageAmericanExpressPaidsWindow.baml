   M S B A M L   `   `   `  ����L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  5      8    2MyDentApplication.ManageAmericanExpressPaidsWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    /localclr-namespace:MyDentApplication    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     60   -   5          �Name$  Window��5       � Title$( "Administrar pagos American Express��5      $	��652��5      $	��496��6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6    � WindowStartupLocation$ CenterScreen=�6S    � 	Resources 5      *   $MyDentApplication.DateValueConverter   DateValueConverter��%   &	          5	      6   5
      .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dgAmericanExpressPaids��5      $��10,88,12.917,48.469q�6-      
IsReadOnly .    CanUserSortColumns . 	   AutoGenerateColumns	 .  
   SelectionMode$
 Single=�5         CanUserAddRows .     CanUserDeleteRows .  $��1��5      ��5      �� *ObservableData+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Día del pago��6      Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.15* 6/   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5      `� 5       `� 	Converter# [  6   .1�5   	   ��  �� Path$ PaidDate�6   5   	   5        5      $ Establecimiento��6   $ 0.15* 6T    62   �� *Establishment+6T     5      $ Concepto��6   $
 0.2* 6G    6+   �� *	Concept+6G     5      $ Total��6V   $
 0.1* 6e    6%   �� *Total+ �� StringFormat$ ${0:n}��6e   5      �� 5      -   5         ��Name$ btnDeletePaid��5      $��Eliminar��6"   $��
140,0,0,10q�65    � Cursor$
 Hand��6I   $��4��6W   $��29��6d   $��Bottom=�6p   $
��Left=�6�   $��60��6�   6�   �� 5      -   5        $ btnEditPaid��5      $��Editar��6    $��	75,0,0,10q�61   $
 Hand��6D   $��3��6R   $
��Left=�6_   $��60��6z   $��29��6�   $��Bottom=�6�   6�   �� 5      -   5        $ 
btnAddPaid��5      $��Agregar��6   $
��Left=�61   $��	10,0,0,10q�6L   $��60��6_   $
 Hand��6j   $��2��6x   $��29��6�   $��Bottom=�6�   6�   �� 5       -   5         ��Name$ lblTotal��5    
   $�� ��6   $��205,0,10,10q�6'   $��Right=�6<   $��29��6_   $��Bottom=�6k   (  "System.Windows.Controls.DatePicker  5!      -   5          Name$ dpStartDate��5!      $
��Left=�6$   $��
108,10,0,0q�6?   $	��Top=�6S   $��140.64��6k     5"      -	   5        $ 	dpEndDate��5"      $
��Left=�6"   $��
108,40,0,0q�6=   $	��Top=�6Q   $��140.64��6i   �� 5#      $��Fecha de inicio:��6
   $
��Left=�6%   $��24��6@   $��	10,10,0,0q�6L   $	��Top=�6_   $��93��6w   �� 5$      $��Fecha de fin:��6
   $
��Left=�6"   $��24��6=   $��25.333,39,0,0q�6I   $	��Top=�6`   $��77.667��6x   �� 5%      -
   5        $ 
btnRefresh��5%      $��256.64,24,0,0q�6   $
 Hand��66   $
��Left=�6D   $��25��6_   $��25��6j   $	��Top=�6v   $��2��6�   .��5&      �� $:��4/MyDentApplication;component/Images/refresh_icon.png��6   $
��Fill=�6I   $��20��6X   5'      5(      5)      