   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      L HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication 5      ,   &MyDentApplication.ManagePatientsWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      9System&clr-namespace:System;assembly=mscorlib     5      1localclr-namespace:MyDentApplication     5      -   5          �Name$  Window��5       � Title$ Administrar pacientes��5      $	��652��5	      $��504.955��6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6    � WindowStartupLocation$ CenterScreen=�6W    � 	Resources 5
      *  $MyDentApplication.BoolValueConverter   BoolValueConverter��%   &	          5   
   6#   5      .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5      �� -   5         ��Name$ btnAddPatient��5      $��Agregar��6"   $��
0,0,140,10q�64    � Cursor$
 Hand��6H   $��5��6V   $��29��6c   $��Bottom=�6o   $��Right=�6�   $��60��6�   6�   �� 5      -   5        $ 	btnDelete��5      $��Eliminar��6   $��	0,0,10,10q�61   $
 Hand��6D   $��6��6R   $��29��6_   $��Bottom=�6k   $��Right=�6�   $��60��6�   6�   �� 5      -   5        $ btnViewAllPatients��5      $��63.333��6'   $	��Top=�66   $��0,14.5,10,0q�6N   $��25��6c   $��Right=�6o   $��Ver todo��6�   $
 Hand��6�   $��3��6�   �� 5      -   5         ��Name$ cbFilter��5      $��26��6   $��84,16.02,0,0q�6+   $	��Top=�6A   $��Normal"�6Y   $��1��6m   $
��Left=�6z   $��100.5��6�   	 �� SelectedIndex$	 0��6�   .@�5      ��	Nombre(s)��6   ��5      Apellido(s)��6    ��5      Exp. No.��6   5      �� 5      $��69��6
   $	��Top=�6   $��10,14.02,0,0q�6-   $��27��6C   $
��Left=�6O   $��Buscar por:��6j   �� 5      -   5        
 ��Name$
 txtSearchTerm��5      $
��Left=�6#   $��25.02��6>   $��
192,16,0,0q�6M    �� TextWrapping$
 Wrap=�6a   $	��Top=�6u   $	��166��6�   
���   �$��1��6�   �� 5      -   5        $ btnRefreshPatients��5      $��
363,16,0,0q�6'   $
 Hand��6;   $
��Left=�6I   $��25��6d   $��25��6o   $	��Top=�6{   $��2��6�   .��5      �� $:��4/MyDentApplication;component/Images/refresh_icon.png��6   $
��Fill=�6I   $��20��6X   5      �� 5      -	   5        $ btnEditPatient��5      $��Editar��6#   $��	0,0,75,10q�64   $
 Hand��6G   $��5��6U   $��29��6b   $��Bottom=�6n   $��Right=�6�   $��60��6�   6�   �� 5      -
   5        $ btnUpdateHc��5      $��Actualizar HC��6    $��127.667,0,0,10q�68   $
 Hand��6P   $��5��6^   $��29��6k   $��Bottom=�6w   $
��Left=�6�   $��90��6�   6�   �� 5      -   5         ��Name$ 
tcPatients��5      $��10,55.353,10,44q�6#   $	 0��6<   .@�5      �� $��Pacientes no asegurados��6   .��5      � ���	#FFE5E5E5.+�5       &   System.Windows.Controls.DataGrid  -   5          Name$ dgPatientsNoHI��5       $��10q�6(      
IsReadOnly .    CanUserSortColumns .    AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  $��4��5"   !   ��5!      �� *ObservableData+   Columns 5#      0  *System.Windows.Controls.DataGridTextColumn  5$      ,  &System.Windows.Controls.DataGridColumn   Header$ Exp. No.��61      Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.085* 6b   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   �� *
AssignedId+6b     5%   	   $ 	Nombre(s)��6   $ 0.14* 6M    6/   �� *	FirstName+6M     5&   	   $ Apellido(s)��6   $ 0.14* 6N    61   �� *
LastName+6N     5'   	   $ Email��6   $
 0.1* 6E    6+   �� *Email+6E     5(   	   $ Teléfono celular��6   $ 0.15* 6T    66   �� *	CellPhone+6T     5)   	   $ Teléfono de casa��6   $ 0.15* 6T    66   �� *	HomePhone+6T   4  .System.Windows.Controls.DataGridCheckBoxColumn  5*   	   $ HC��6    $ 0.04* 6,    5+   
   `� 5,       `� 	Converter# [  6   .1�5-      ��  �� Path$ ClinicHistoryId�6   5.      50   
   52      53      54      �� 55      $��Pacientes asegurados��6   .��56      � ���	#FFE5E5E5.+�57        -   5        $ dgPatientsWithHI��57      $��10q�6*    .  .  .   .   .  $��4��59   !   ��58      �� *ObservableData+ 5:        5;      $ Exp. No.��61   $ 0.085* 6b    6C   �� *
AssignedId+6b     5<   	   $ 	Nombre(s)��6   $ 0.14* 6M    6/   �� *	FirstName+6M     5=   	   $ Apellido(s)��6   $ 0.14* 6N    61   �� *
LastName+6N     5>   	   $ Email��6   $
 0.1* 6E    6+   �� *Email+6E     5?   	   $ Teléfono celular��6   $ 0.15* 6T    66   �� *	CellPhone+6T     5@   	   $ Teléfono de casa��6   $ 0.15* 6T    66   �� *	HomePhone+6T     5A   	   $ HC��6    $ 0.04* 6,    5B   
   `� 5C      # [  6   .1�5D      �� $ ClinicHistoryId�6   5E      5G   
   5I      5J      5K      �� 5L      $#��Pacientes asegurados diversos��6   .��5M      � ���	#FFE5E5E5.+�5N        -   5        $ dgPatientsDiverse��5N      $��10q�6+    .  .  .   .   .  $��4��5P   !   ��5O      �� *ObservableData+ 5Q        5R      $ Exp. No.��61   $ 0.085* 6b    6C   �� *
AssignedId+6b     5S   	   $ 	Nombre(s)��6   $ 0.14* 6M    6/   �� *	FirstName+6M     5T   	   $ Apellido(s)��6   $ 0.14* 6N    61   �� *
LastName+6N     5U   	   $ Email��6   $
 0.1* 6E    6+   �� *Email+6E     5V   	   $ Teléfono celular��6   $ 0.15* 6T    66   �� *	CellPhone+6T     5W   	   $ Teléfono de casa��6   $ 0.15* 6T    66   �� *	HomePhone+6T     5X   	   $ HC��6    $ 0.04* 6,    5Y   
   `� 5Z      # [  6   .1�5[      �� $ ClinicHistoryId�6   5\      5^   
   5`      5a      5b      5c      �� 5d      -   5        $ btnAuthorization��5d      $��Autorización��6%   $��222.667,0,0,10q�6<   $
 Hand��6T   $��5��6b   $��29��6o   $��Bottom=�6{   $
��Left=�6�   $��90��6�   {�.  6�   �� 5e      -   5        $ btnViewStatements��5e      $��Estados de cuenta��6&   $��	10,0,0,10q�6B   $
 Hand��6U   $��5��6c   $��29��6p   $��Bottom=�6|   $
��Left=�6�   $��112.667��6�   6�   5f      5g      