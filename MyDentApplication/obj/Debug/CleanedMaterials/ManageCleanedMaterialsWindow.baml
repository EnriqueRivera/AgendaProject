   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      L HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication 5      4   .MyDentApplication.ManageCleanedMaterialsWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      9System&clr-namespace:System;assembly=mscorlib     5      1localclr-namespace:MyDentApplication     5      -   5          �Name$  Window��5       � Title$* $Administrar limpieza de instrumentos��5      $��814.716��5	      $	��459��6    � WindowStartupLocation$ CenterScreen=�6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6D    � 	Resources 5
      *  $MyDentApplication.DateValueConverter   DateValueConverter*  $MyDentApplication.BoolValueConverter  BoolValueConverter��%:   &	        &	        5      6     5   
   6#   5      .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dgCleanedMaterials��5      $��10,10,10,44q�6)      
IsReadOnly .    CanUserSortColumns . 	   AutoGenerateColumns	 .  
   SelectionMode$
 Single=�5         CanUserAddRows .     CanUserDeleteRows .  $��1��5      ��5      �� *ObservableData+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Fecha��6      Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.1* 6(   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5      `� 5       `� 	Converter# [  6   .1�5   	   ��  �� Path$ CreatedDate�6   5   	   5        5      $ Grupo��6   $ 0.04* 6H    6(   �� *GroupLetter+6H     5      $ Observaciones��6   $
 0.2* 6Q    60   �� *Observations+6Q   4  .System.Windows.Controls.DataGridCheckBoxColumn  5      $ Lavado��6   $ 0.07* 6-    5      `� 5      # [ 6   .1�5    	   �� $ Cleaned�6   5!   	   5#        5$      $ Empaquetado��6   $ 0.07* 62    5%      `� 5&      # [ 6   .1�5'   	   �� $ Packaged�6   5(   	   5*        5+      $ Esterilizado��6   $ 0.07* 63    5,      `� 5-      # [ 6   .1�5.   	   �� $ 
Sterilized�6   5/   	   51      53      �� 54      -   5         ��Name$ btnDeleteCleanedMaterial��54      $��Eliminar��6-   $��	0,0,10,10q�6@    � Cursor$
 Hand��6S   $��5��6a   $��29��6n   $��Bottom=�6z   $��Right=�6�   $��60��6�   6�   �� 55      -   5        $ btnAddCleanedMaterial��55      $��Agregar��6*   $��Right=�6<   $��
0,0,140,10q�6X   $��60��6l   $
 Hand��6w   $��3��6�   $��29��6�   $��Bottom=�6�   6�   �� 56      -   5        $ btnEditCleanedMaterial��56      $��Editar��6+   $��Right=�6<   $��	0,0,75,10q�6X   $��60��6k   $
 Hand��6v   $��3��6�   $��29��6�   $��Bottom=�6�   6�   �� 57      -   5        $ btnMarkSterilized��57      $��Esterilizado��6&   $��
244,0,0,10q�6=   $
 Hand��6Q   $��5��6_   $��29��6l   $��Bottom=�6x   $
��Left=�6�   $��76��6�   6�   �� 58      -   5        $ btnMarkCleaned��58      $��Lavado��6#   $
��Left=�64   $��	94,0,0,10q�6O   $��60��6b   $
 Hand��6m   $��3��6{   $��29��6�   $��Bottom=�6�   �� 59      -	   5        $ btnMarkPackaged��59      $��Empaquetado��6$   $
��Left=�6:   $��
159,0,0,10q�6U   $��80��6i   $
 Hand��6t   $��3��6�   $��29��6�   $��Bottom=�6�   �� 5:      $��Marcar como:��6
   $
��Left=�6!   $��	10,0,0,10q�6<   $��84��6O   $��29��6Z   $��Bottom=�6f   �� 5;      -
   5        $ btnViewCleanedMaterial��5;      $��Ver detalles��6+   $��Right=�6B   $��
0,0,205,10q�6^   $��74��6r   $
 Hand��6}   $��3��6�   $��29��6�   $��Bottom=�6�   6�   �� 5<      -   5        $ btnMarkReSterilized��5<      $��Re-Esterilizado��6(   $��
325,0,0,10q�6B   $
 Hand��6V   $��5��6d   $��29��6q   $��Bottom=�6}   $
��Left=�6�   $��93��6�   6�   5=      5>      