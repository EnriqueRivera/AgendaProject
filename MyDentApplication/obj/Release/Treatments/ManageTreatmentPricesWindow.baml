   M S B A M L   `   `   `  ���� L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  3    -MyDentApplication.ManageTreatmentPricesWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     1localclr-namespace:MyDentApplication     X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -     �Name$  Window�� � Title$( "Administrar costos de tratamientos��$	��652��$	��496�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � WindowStartupLocation$ CenterScreen=� � 	Resources *   $MyDentApplication.DateValueConverter   DateValueConverter��%   &	          .��� -    �Name$ 
LayoutRoot��.+��� -    ��Name$ tcTratments��$��10,49.917,10,44q� �� SelectedIndex$ 0��.@��� $��Odontología��$��-2,-2,-0.315,-1q�.��� ���	#FFE5E5E5.+�&   System.Windows.Controls.DataGrid  -     Name$ dgDentristy��$��10q�	   
IsReadOnly	 . 
   CanUserSortColumns
 .    AutoGenerateColumns .     SelectionMode$ Single=�   CanUserAddRows .     CanUserDeleteRows .  $��1������ *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Clave��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.15* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *TreatmentKey+  $ Nombre del tratamiento��$
 0.6*  �� *Name+  $ Fecha de creación��$ 0.26*  `�  `� 	Converter# [  .1���  �� Path$ CreatedDate�  $ Precio��$
 0.2*  �� *Price+ �� StringFormat$ ${0:n}��  $ Descuento (%)��$ 0.22*  �� *
Discount+�� $��Clínica del dolor��$��-2,-2,-2,-1q�.��� ���	#FFE5E5E5.+��� -   $ tcPainClinic��$��10q�.@��� $��Costos vigentes��.��� ���	#FFE5E5E5.+�  -   $ dgPainClinic��$��10q�	 . 
 .  .  $ Single=� .   .  $��1������ *ObservableData+   $ Clave��$ 0.15*  �� *TreatmentKey+  $ Nombre del tratamiento��$
 0.6*  �� *Name+  $ Fecha de creación��$ 0.26*  `� # [  .1��� $ CreatedDate�  $ Precio��$
 0.2*  �� *Price+$ ${0:n}���� $��Pacientes asegurados��.��� ���	#FFE5E5E5.+�  -   $ dgPainClinicHI��$��10q�	 . 
 .  .  $ Single=� .   .  $��1������ *ObservableData+   $ Clave��$ 0.15*  �� *TreatmentKey+  $ Nombre del tratamiento��$
 0.6*  �� *Name+  $ Fecha de creación��$ 0.26*  `� # [  .1��� $ CreatedDate�  $ Precio��$
 0.2*  �� *Price+$ ${0:n}���� $��
Endodoncia��$��-2,-2,-2,-1q�.��� ���	#FFE5E5E5.+��� -   $ tcEndodontics��$��10q�.@��� $��Costos vigentes��.��� ���	#FFE5E5E5.+�  -	   $ dgEndodontics��$��10q�	 . 
 .  .  $ Single=� .   .  $��1������ *ObservableData+   $ Clave��$ 0.15*  �� *TreatmentKey+  $ Nombre del tratamiento��$
 0.6*  �� *Name+  $ Fecha de creación��$ 0.26*  `� # [  .1��� $ CreatedDate�  $ Precio��$
 0.2*  �� *Price+$ ${0:n}���� $��Pacientes asegurados��.��� ���	#FFE5E5E5.+�  -
   $ dgEndodonticsHI��$��10q�	 . 
 .  .  $ Single=� .   .  $��1������ *ObservableData+   $ Clave��$ 0.15*  �� *TreatmentKey+  $ Nombre del tratamiento��$
 0.6*  �� *Name+  $ Fecha de creación��$ 0.26*  `� # [  .1��� $ CreatedDate�  $ Precio��$
 0.2*  �� *Price+$ ${0:n}���� $��
Ortodoncia��$��-2,-2,-2,-1q�.��� ���	#FFE5E5E5.+��� -   $ tcOrthodontics��$��10q�.@��� $��Costos vigentes��.��� ���	#FFE5E5E5.+�  -   $ dgOrthodontics��$��10q�	 . 
 .  .  $ Single=� .   .  $��1������ *ObservableData+   $ Clave��$ 0.15*  �� *TreatmentKey+  $ Nombre del tratamiento��$
 0.6*  �� *Name+  $ Fecha de creación��$ 0.26*  `� # [  .1��� $ CreatedDate�  $ Precio��$
 0.2*  �� *Price+$ ${0:n}���� $��Pacientes asegurados��.��� ���	#FFE5E5E5.+�  -   $ dgOrthodonticsHI��$��10q�	 . 
 .  .  $ Single=� .   .  $��1������ *ObservableData+   $ Clave��$ 0.15*  �� *TreatmentKey+  $ Nombre del tratamiento��$
 0.6*  �� *Name+  $ Fecha de creación��$ 0.26*  `� # [  .1��� $ CreatedDate�  $ Precio��$
 0.2*  �� *Price+$ ${0:n}���� $	��CMF��$��-2,-2,-2,-1q�.��� ���	#FFE5E5E5.+��� -   $ tcCmf��$��10q�.@��� $��Costos vigentes��.��� ���	#FFE5E5E5.+�  -   $ dgCmf��$��10q�	 . 
 .  .  $ Single=� .   .  $��1������ *ObservableData+   $ Clave��$ 0.15*  �� *TreatmentKey+  $ Nombre del tratamiento��$
 0.6*  �� *Name+  $ Fecha de creación��$ 0.26*  `� # [  .1��� $ CreatedDate�  $ Precio��$
 0.2*  �� *Price+$ ${0:n}���� $��Pacientes asegurados��.��� ���	#FFE5E5E5.+�  -   $ dgCmfHI��$��10q�	 . 
 .  .  $ Single=� .   .  $��1������ *ObservableData+   $ Clave��$ 0.15*  �� *TreatmentKey+  $ Nombre del tratamiento��$
 0.6*  �� *Name+  $ Fecha de creación��$ 0.26*  `� # [  .1��� $ CreatedDate�  $ Precio��$
 0.2*  �� *Price+$ ${0:n}���� $��Periodoncia��$��-2,-2,-2,-1q�.��� ���	#FFE5E5E5.+��� -   $ tcPeriodontics��$��10q�.@��� $��Costos vigentes��.��� ���	#FFE5E5E5.+�  -   $ dgPeriodontics��$��10q�	 . 
 .  .  $ Single=� .   .  $��1������ *ObservableData+   $ Clave��$ 0.15*  �� *TreatmentKey+  $ Nombre del tratamiento��$
 0.6*  �� *Name+  $ Fecha de creación��$ 0.26*  `� # [  .1��� $ CreatedDate�  $ Precio��$
 0.2*  �� *Price+$ ${0:n}���� $��Pacientes asegurados��.��� ���	#FFE5E5E5.+�  -   $ dgPeriodonticsHI��$��10q�	 . 
 .  .  $ Single=� .   .  $��1������ *ObservableData+   $ Clave��$ 0.15*  �� *TreatmentKey+  $ Nombre del tratamiento��$
 0.6*  �� *Name+  $ Fecha de creación��$ 0.26*  `� # [  .1��� $ CreatedDate�  $ Precio��$
 0.2*  �� *Price+$ ${0:n}���� $��Odontopediatría��$��-2,-2,-2,-1q�.��� ���	#FFE5E5E5.+��� -   $ tcPediatricDental��$��10q�.@��� $��Costos vigentes��.��� ���	#FFE5E5E5.+�  -   $ dgPediatricDental��$��10q�	 . 
 .  .  $ Single=� .   .  $��1������ *ObservableData+   $ Clave��$ 0.15*  �� *TreatmentKey+  $ Nombre del tratamiento��$
 0.6*  �� *Name+  $ Fecha de creación��$ 0.26*  `� # [  .1��� $ CreatedDate�  $ Precio��$
 0.2*  �� *Price+$ ${0:n}���� $��Pacientes asegurados��.��� ���	#FFE5E5E5.+�  -   $ dgPediatricDentalHI��$��10q�	 . 
 .  .  $ Single=� .   .  $��1������ *ObservableData+   $ Clave��$ 0.15*  �� *TreatmentKey+  $ Nombre del tratamiento��$
 0.6*  �� *Name+  $ Fecha de creación��$ 0.26*  `� # [  .1��� $ CreatedDate�  $ Precio��$
 0.2*  �� *Price+$ ${0:n}���� -    ��Name$ btnDeleteTreatment��$��Eliminar��$��	0,0,10,10q� � Cursor$
 Hand��$��4��$��29��$��Bottom=�$��Right=�$��60���� -   $ btnEditTreatment��$��Editar��$��	0,0,75,10q�$
 Hand��$��3��$��Right=�$��60��$��29��$��Bottom=��� -   $ btnAddTreatment��$��Agregar��$��Right=�$��
0,0,140,10q�$��60��$
 Hand��$��2��$��29��$��Bottom=��� -   $ btnCopyPrices��$5��/Copiar precios del año pasado al año en curso��$��	10,0,0,10q�$
 Hand��$��2��$��29��$��Bottom=�$
��Left=�$	��262��&   Xceed.Wpf.Toolkit.DateTimeUpDown  -     Name$ dtudSelectedYear��$
��Left=�$
��25.5��$��
133,10,0,0q�$	��Top=�$��187.5��   Format$ Custom=�   FormatString$
 yyyy�� K� ZIndex$ 1���� $	��118��$	��Top=�$��	10,10,0,0q�$��27��$
��Left=�$��Seleccione un año:���� -   $ 
btnRefresh��$��325.5,10,0,0q�$
 Hand��$
��Left=�$��25��$��25��$	��Top=�.���� $:��4/MyDentApplication;component/Images/refresh_icon.png��$
��Fill=�$��20��