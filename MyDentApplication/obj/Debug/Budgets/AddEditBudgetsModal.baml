   M S B A M L   `   `   `  ����L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  5      +    %MyDentApplication.AddEditBudgetsModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      1localclr-namespace:MyDentApplication     5      -   5          �Name$  Window��5       � Title$ Agregar nuevo presupuesto��5      $	��640��5      $	��555��6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6    � WindowStartupLocation$ CenterScreen=�6S    � WindowState$ 	Maximized=�6x   $	��640��6�   $	��555��6�    � 	Resources 5
      *   $MyDentApplication.DateValueConverter   DateValueConverter��%   &	          5      6   5      .��5      � -   5         �Name$ 
LayoutRoot��5       � RowDefinitions 5      �� 5      $	��86* �6   �� 5      $
��252* �6   �� 5      $
��143* �6   �� 5      $	��46* �6   .+�5      �� -   5         ��Name$ lblBudgetName��5   
   $��Nombre del presupuesto:��6!   $
��Left=�6C   $��27��6^   $��	10,10,0,0q�6j   $	��Top=�6}   $��147.167��6�   �� 5      -   5        	 ��Name$	 txtBudgetName��5      $��27��6#   $��162.167,10,111.333,0q�6/   
 �� TextWrapping$

 Wrap=�6M   $	��Top=�6a   $��1��6y   $��1q�6�   
���   �$��Center=�6�   �� 5      -   5        $ lblPatientName��5   
   $��	Paciente:��6"   $
��Left=�66   $��27��6Q   $��	97,51,0,0q�6]   $	��Top=�6p   $��60.167��6�   �� 5      -   5        $	 txtPatientName��5      $��27��6$   $��162.167,51,111.333,0q�60   $

 Wrap=�6N   $	��Top=�6b   $��2��6z   $��1q�6�   
���   �{�.  6�   &   System.Windows.Controls.DataGrid  5      -   5          Name$ dgBudgetDetails��5      $��4��6&      SelectionMode$ Single=�63   $��10,8.299,75,9q�6J      
IsReadOnly .    CanUserAddRows .     CanUserSortColumns .    CanUserDeleteRows .     AutoGenerateColumns .  $��1��6  ��6a   �� *ObservableData+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Cantidad��66      Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.15* 6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6   �� *
Quantity+6H     5      $ Concepto��65   $
 0.2* 6G    6   �� *	Concept+6G     5      $ 	No. citas��6<   $ 0.15* 6O    6   �� *NumberOfEvents+6O     5      $ Costo u. normal��6Y   $
 0.2* 6r    6%   �� *
UnitCost+ �� StringFormat$ ${0:n}��6r     5      $ Costo u. desc.��6a   $
 0.2* 6y    6%   �� *UnitCostDiscount+$ ${0:n}��6y     5       $ 
Total Neto��6Y   $ 0.16* 6m    6%   �� *
NetTotal+$ ${0:n}��6m     5!      $ Total con desc.��6^   $ 0.16* 6w    6%   �� *TotalDiscount+$ ${0:n}��6w     5"      $ Descuento (%)��66   $ 0.16* 6M    6   �� *
Discount+6M     5#      $ Total por cita��6^   $ 0.16* 6v    6%   �� *TotalPerEvent+$ ${0:n}��6v   5%      �� 5&      -   5         ��Name$ btnFindPatient��5&      $��Right=�6#   $��30��6?   $��0,51,76.333,0q�6K   $	��Top=�6b   $��30��6z    � Cursor$
 Hand��6�   $��3��6�   .��5'      �� $:��4/MyDentApplication;component/Images/patient_icon.png��6   $
��Fill=�6I   5(      �� 5)      -	   5        $ lblNotes��5)   
   $��Observaciones:��6   $
��Left=�65   $��25.672��6P   $��10,0,0,0q�6`   $	��Top=�6r   $��95.408��6�   $��2��6�   �� 5*      $��1q�6   
���   �$��10,25.672,365.5,0q�63   $��2��6N   $��77.028��6[   $	��Top=�6k   ._�5+      �� -
   5        $	 txtNotes��5+      $��0,0,0,0q�6   $��Italic$�60   " �� VerticalScrollBarVisibility$
 Auto=�6C   $

 Wrap=�6f   $��8��6z   5,      �� 5-      -   5        $ lblPatientName_Copy��5-   
   $��No. de citas:��6'   $��Right=�6?   $��27��6[   $��0,12.329,184.833,0q�6g   $	��Top=�6�   $��76.167��6�   $��2��6�   �� 5.      -   5        $	 txtTotalNumberOfEvents��5.      $��27��6,   $��0,11.5,76.333,0q�68   $

 Wrap=�6Q   $	��Top=�6e   $��1��6}   $��1q�6�   
���   �{�.  $��2��6�   $��Right=�6�   $��103.5��6�   �� 5/      -   5        $ btnDeleteBudgetDetail��5/      $��Bottom=�6*   $��7��6E   $��	0,0,10,10q�6R   $��29��6e   $��Eliminar��6q   $
 Hand��6�   $��Right=�6�   $��60��6�   $��1��6�   �� 50      -   5        $ btnEditBudgetDetail��50      $��Bottom=�6(   $��6��6C   $��	0,0,10,44q�6P   $��29��6c   $��Editar��6o   $
 Hand��6�   $��Right=�6�   $��60��6�   $��1��6�   �� 51      -   5        $ btnAddBudgetDetail��51      $��60��6'   $��Bottom=�62   $��5��6M   $��	0,0,10,78q�6Z   $��29��6m   $��Right=�6y   $��Agregar��6�   $
 Hand��6�   $��1��6�   �� 52      -   5        $ lblPatientName_Copy1��52   
   $��Gran total: $��6(   $��Right=�6@   $��27��6\   $��0,43.5,179.833,0q�6h   $	��Top=�6�   $
��75.5��6�   $��2��6�   �� 53      -   5        $	 txtGrandTotal��53      $��27��6#   $��0,43.5,76.333,0q�6/   $

 Wrap=�6H   $	��Top=�6\   $��1��6t   $��1q�6�   
���   �{�.  $��2��6�   $��Right=�6�   $��103.5��6�   �� 54      -   5        $ lblPatientName_Copy2��54   
   $��Gran total con desc: $��6(   $��Right=�6I   $��27��6e   $��0,75.5,179.833,0q�6q   $	��Top=�6�   $��124.5��6�   $��2��6�   �� 55      -   5        $	 txtGrandTotalDiscount��55      $��27��6+   $��0,75.5,76.333,0q�67   $

 Wrap=�6P   $	��Top=�6d   $��1��6|   $��1q�6�   
���   �{�.  $��2��6�   $��Right=�6�   $��103.5��6�   �� 56      -   5        $ btnAddUpdateBudget��56      $��Crear presupuesto��6'   $��Right=�6C   $��
0,0,106,10q�6_   $	��145��6s   $
 Hand��6   $��10��6�   $��29��6�   $��Bottom=�6�    K� ZIndex$ 6��6�   $��3��6�   6�   �� 57      -   5        $ 	btnCancel��57      $��Cancelar��6   $��Right=�61   $��	0,0,10,10q�6M   $��91��6`   $
 Hand��6k   $��11��6y   $��29��6�   $��Bottom=�6�   $ 7��6�   $��3��6�   6�   �� 58      -   5        $ 
lblExpDate��58   
   $��	Vigencia:��6   $
��Left=�62   $��27��6M   $��9.334,114.5,0,0q�6Y   $	��Top=�6r   $��59.666��6�   $��2��6�   (  "System.Windows.Controls.DatePicker  59      -   5          Name$ 
dtpExpDate��59      $��27��6#   $��74,114.5,0,0q�6/   $	��Top=�6E   $��9��6]   $��2��6j   $
��Left=�6w   $��192.5��6�   �� 5:      -   5        $ btnExportPdf��5:      $��Generar PDF��6!   $��	0,0,10,10q�67   $
 Hand��6J   $��11��6X   $��29��6f   $��Bottom=�6r   $ 7��6�   $��3��6�   $��Right=�6�   $��91��6�   $y�Hidden=�6�   5;      5<      