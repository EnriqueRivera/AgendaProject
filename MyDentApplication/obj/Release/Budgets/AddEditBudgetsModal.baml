   M S B A M L   `   `   `  ���� L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  +    %MyDentApplication.AddEditBudgetsModal   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     1localclr-namespace:MyDentApplication     -     �Name$  Window�� � Title$ Agregar nuevo presupuesto��$	��640��$	��555�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=�$	��640��$	��555�� � 	Resources *   $MyDentApplication.DateValueConverter   DateValueConverter��%   &	          .��� -    �Name$ 
LayoutRoot�� � RowDefinitions �� $	��86* ��� $
��252* ��� $
��143* ��� $	��46* �.+��� -    ��Name$ lblBudgetName��$��Nombre del presupuesto:��$
��Left=�$��27��$��	10,10,0,0q�$	��Top=�$��147.167���� -   	 ��Name$	 txtBudgetName��$��27��$��162.167,10,111.333,0q�
 �� TextWrapping$

 Wrap=�$	��Top=�$��1��$��1q�
���   �$��Center=��� -   $ lblPatientName��$��	Paciente:��$
��Left=�$��27��$��	97,51,0,0q�$	��Top=�$��60.167���� -   $	 txtPatientName��$��27��$��162.167,51,111.333,0q�$

 Wrap=�$	��Top=�$��2��$��1q�
���   �{�.  &   System.Windows.Controls.DataGrid  -     Name$ dgBudgetDetails��$��4��   SelectionMode$ Single=�$��10,8.299,75,9q�   
IsReadOnly .    CanUserAddRows .     CanUserSortColumns .    CanUserDeleteRows .     AutoGenerateColumns .  $��1������ *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Cantidad��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.15* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
Quantity+  $ Concepto��$
 0.2*  �� *	Concept+  $ 	No. citas��$ 0.15*  �� *NumberOfEvents+  $ Costo u. normal��$
 0.2*  �� *
UnitCost+ �� StringFormat$ ${0:n}��  $ Costo u. desc.��$
 0.2*  �� *UnitCostDiscount+$ ${0:n}��  $ 
Total Neto��$ 0.16*  �� *
NetTotal+$ ${0:n}��  $ Total con desc.��$ 0.16*  �� *TotalDiscount+$ ${0:n}��  $ Descuento (%)��$ 0.16*  �� *
Discount+  $ Total por cita��$ 0.16*  �� *TotalPerEvent+$ ${0:n}���� -    ��Name$ btnFindPatient��$��Right=�$��30��$��0,51,76.333,0q�$	��Top=�$��30�� � Cursor$
 Hand��$��3��.���� $:��4/MyDentApplication;component/Images/patient_icon.png��$
��Fill=��� -	   $ lblNotes��$��Observaciones:��$
��Left=�$��25.672��$��10,0,0,0q�$	��Top=�$��95.408��$��2���� $��1q�
���   �$��10,25.672,365.5,0q�$��2��$��77.028��$	��Top=�._��� -
   $	 txtNotes��$��0,0,0,0q�$��Italic$�" �� VerticalScrollBarVisibility$
 Auto=�$

 Wrap=�$��8���� -   $ lblPatientName_Copy��$��No. de citas:��$��Right=�$��27��$��0,12.329,184.833,0q�$	��Top=�$��76.167��$��2���� -   $	 txtTotalNumberOfEvents��$��27��$��0,11.5,76.333,0q�$

 Wrap=�$	��Top=�$��1��$��1q�
���   �{�.  $��2��$��Right=�$��103.5���� -   $ btnDeleteBudgetDetail��$��Bottom=�$��7��$��	0,0,10,10q�$��29��$��Eliminar��$
 Hand��$��Right=�$��60��$��1���� -   $ btnEditBudgetDetail��$��Bottom=�$��6��$��	0,0,10,44q�$��29��$��Editar��$
 Hand��$��Right=�$��60��$��1���� -   $ btnAddBudgetDetail��$��60��$��Bottom=�$��5��$��	0,0,10,78q�$��29��$��Right=�$��Agregar��$
 Hand��$��1���� -   $ lblPatientName_Copy1��$��Gran total: $��$��Right=�$��27��$��0,43.5,179.833,0q�$	��Top=�$
��75.5��$��2���� -   $	 txtGrandTotal��$��27��$��0,43.5,76.333,0q�$

 Wrap=�$	��Top=�$��1��$��1q�
���   �{�.  $��2��$��Right=�$��103.5���� -   $ lblPatientName_Copy2��$��Gran total con desc: $��$��Right=�$��27��$��0,75.5,179.833,0q�$	��Top=�$��124.5��$��2���� -   $	 txtGrandTotalDiscount��$��27��$��0,75.5,76.333,0q�$

 Wrap=�$	��Top=�$��1��$��1q�
���   �{�.  $��2��$��Right=�$��103.5���� -   $ btnAddUpdateBudget��$��Crear presupuesto��$��Right=�$��
0,0,106,10q�$	��145��$
 Hand��$��10��$��29��$��Bottom=� K� ZIndex$ 6��$��3���� -   $ 	btnCancel��$��Cancelar��$��Right=�$��	0,0,10,10q�$��91��$
 Hand��$��11��$��29��$��Bottom=�$ 7��$��3���� -   $ 
lblExpDate��$��	Vigencia:��$
��Left=�$��27��$��9.334,114.5,0,0q�$	��Top=�$��59.666��$��2��(  "System.Windows.Controls.DatePicker  -     Name$ 
dtpExpDate��$��27��$��74,114.5,0,0q�$	��Top=�$��9��$��2��$
��Left=�$��192.5���� -   $ btnExportPdf��$��Generar PDF��$��	0,0,10,10q�$
 Hand��$��11��$��29��$��Bottom=�$ 7��$��3��$��Right=�$��91��$y�Hidden=�