   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  L HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication 4   .MyDentApplication.ManageCleanedMaterialsWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9System&clr-namespace:System;assembly=mscorlib     1localclr-namespace:MyDentApplication     -     �Name$  Window�� � Title$* $Administrar limpieza de instrumentos��$��814.716��$	��459�� � WindowStartupLocation$ CenterScreen=� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � 	Resources *  $MyDentApplication.DateValueConverter   DateValueConverter*  $MyDentApplication.BoolValueConverter  BoolValueConverter��%   &	        &	          .��� -    �Name$ 
LayoutRoot��.+�&   System.Windows.Controls.DataGrid  -     Name$ dgCleanedMaterials��$��10,10,10,44q�   
IsReadOnly .    CanUserSortColumns . 	   AutoGenerateColumns	 .  
   SelectionMode$
 Single=�   CanUserAddRows .     CanUserDeleteRows .  $��1������ *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Fecha��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.1* 1  +System.Windows.Controls.DataGridBoundColumn   Binding `�  `� 	Converter# [  .1���  �� Path$ CreatedDate�  $ Grupo��$ 0.04*  �� *GroupLetter+  $ Observaciones��$
 0.2*  �� *Observations+4  .System.Windows.Controls.DataGridCheckBoxColumn  $ Lavado��$ 0.07*  `� # [ .1��� $ Cleaned�  $ Empaquetado��$ 0.07*  `� # [ .1��� $ Packaged�  $ Esterilizado��$ 0.07*  `� # [ .1��� $ 
Sterilized��� -    ��Name$ btnDeleteCleanedMaterial��$��Eliminar��$��	0,0,10,10q� � Cursor$
 Hand��$��5��$��29��$��Bottom=�$��Right=�$��60���� -   $ btnAddCleanedMaterial��$��Agregar��$��Right=�$��
0,0,140,10q�$��60��$
 Hand��$��3��$��29��$��Bottom=��� -   $ btnEditCleanedMaterial��$��Editar��$��Right=�$��	0,0,75,10q�$��60��$
 Hand��$��3��$��29��$��Bottom=��� -   $ btnMarkSterilized��$��Esterilizado��$��
244,0,0,10q�$
 Hand��$��5��$��29��$��Bottom=�$
��Left=�$��76���� -   $ btnMarkCleaned��$��Lavado��$
��Left=�$��	94,0,0,10q�$��60��$
 Hand��$��3��$��29��$��Bottom=��� -	   $ btnMarkPackaged��$��Empaquetado��$
��Left=�$��
159,0,0,10q�$��80��$
 Hand��$��3��$��29��$��Bottom=��� $��Marcar como:��$
��Left=�$��	10,0,0,10q�$��84��$��29��$��Bottom=��� -
   $ btnViewCleanedMaterial��$��Ver detalles��$��Right=�$��
0,0,205,10q�$��74��$
 Hand��$��3��$��29��$��Bottom=��� -   $ btnMarkReSterilized��$��Re-Esterilizado��$��
325,0,0,10q�$
 Hand��$��5��$��29��$��Bottom=�$
��Left=�$��93��