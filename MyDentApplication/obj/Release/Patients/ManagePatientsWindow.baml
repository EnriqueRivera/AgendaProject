   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  L HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication ,   &MyDentApplication.ManagePatientsWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9System&clr-namespace:System;assembly=mscorlib     1localclr-namespace:MyDentApplication     -     �Name$  Window�� � Title$ Administrar pacientes��$	��652��$��504.955�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � WindowStartupLocation$ CenterScreen=� � 	Resources *  $MyDentApplication.BoolValueConverter   BoolValueConverter��%   &	          .��� -    �Name$ 
LayoutRoot��.+��� -    ��Name$ btnAddPatient��$��Agregar��$��
0,0,140,10q� � Cursor$
 Hand��$��5��$��29��$��Bottom=�$��Right=�$��60���� -   $ 	btnDelete��$��Eliminar��$��	0,0,10,10q�$
 Hand��$��6��$��29��$��Bottom=�$��Right=�$��60���� -   $ btnViewAllPatients��$��63.333��$	��Top=�$��0,14.5,10,0q�$��25��$��Right=�$��Ver todo��$
 Hand��$��3���� -    ��Name$ cbFilter��$��26��$��84,16.02,0,0q�$	��Top=�$��Normal"�$��1��$
��Left=�$��100.5��	 �� SelectedIndex$	 0��.@���	Nombre(s)����Apellido(s)����Exp. No.���� $��69��$	��Top=�$��10,14.02,0,0q�$��27��$
��Left=�$��Buscar por:���� -   
 ��Name$
 txtSearchTerm��$
��Left=�$��25.02��$��
192,16,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��166��
���   �$��1���� -   $ btnRefreshPatients��$��
363,16,0,0q�$
 Hand��$
��Left=�$��25��$��25��$	��Top=�$��2��.���� $:��4/MyDentApplication;component/Images/refresh_icon.png��$
��Fill=�$��20���� -	   $ btnEditPatient��$��Editar��$��	0,0,75,10q�$
 Hand��$��5��$��29��$��Bottom=�$��Right=�$��60���� -
   $ btnUpdateHc��$��Actualizar HC��$��127.667,0,0,10q�$
 Hand��$��5��$��29��$��Bottom=�$
��Left=�$��90���� -    ��Name$ 
tcPatients��$��10,55.353,10,44q�$	 0��.@��� $��Pacientes no asegurados��.��� ���	#FFE5E5E5.+�&   System.Windows.Controls.DataGrid  -     Name$ dgPatientsNoHI��$��10q�   
IsReadOnly .    CanUserSortColumns .    AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  $��4������ *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Exp. No.��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.085* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
AssignedId+  $ 	Nombre(s)��$ 0.14*  �� *	FirstName+  $ Apellido(s)��$ 0.14*  �� *
LastName+  $ Email��$
 0.1*  �� *Email+  $ Teléfono celular��$ 0.15*  �� *	CellPhone+  $ Teléfono de casa��$ 0.15*  �� *	HomePhone+4  .System.Windows.Controls.DataGridCheckBoxColumn  $ HC��$ 0.04*  `�  `� 	Converter# [  .1���  �� Path$ ClinicHistoryId��� $��Pacientes CENTAURO��.��� ���	#FFE5E5E5.+�  -   $ dgPatientsWithHI��$��10q� .  .  .   .   .  $��4������ *ObservableData+   $ Exp. No.��$ 0.085*  �� *
AssignedId+  $ 	Nombre(s)��$ 0.14*  �� *	FirstName+  $ Apellido(s)��$ 0.14*  �� *
LastName+  $ Email��$
 0.1*  �� *Email+  $ Teléfono celular��$ 0.15*  �� *	CellPhone+  $ Teléfono de casa��$ 0.15*  �� *	HomePhone+  $ HC��$ 0.04*  `� # [  .1��� $ ClinicHistoryId��� $��Pacientes GS��.��� ���	#FFE5E5E5.+�  -   $ dgPatientsDiverse��$��10q� .  .  .   .   .  $��4������ *ObservableData+   $ Exp. No.��$ 0.085*  �� *
AssignedId+  $ 	Nombre(s)��$ 0.14*  �� *	FirstName+  $ Apellido(s)��$ 0.14*  �� *
LastName+  $ Email��$
 0.1*  �� *Email+  $ Teléfono celular��$ 0.15*  �� *	CellPhone+  $ Teléfono de casa��$ 0.15*  �� *	HomePhone+  $ HC��$ 0.04*  `� # [  .1��� $ ClinicHistoryId��� $��Pacientes DENTEGRA��.��� ���	#FFE5E5E5.+�  -   $ dgPatientsDentegra��$��10q� .  .  .   .   .  $��4������ *ObservableData+   $ Exp. No.��$ 0.085*  �� *
AssignedId+  $ 	Nombre(s)��$ 0.14*  �� *	FirstName+  $ Apellido(s)��$ 0.14*  �� *
LastName+  $ Email��$
 0.1*  �� *Email+  $ Teléfono celular��$ 0.15*  �� *	CellPhone+  $ Teléfono de casa��$ 0.15*  �� *	HomePhone+  $ HC��$ 0.04*  `� # [  .1��� $ ClinicHistoryId��� -   $ btnAuthorization��$��Autorización��$��222.667,0,0,10q�$
 Hand��$��5��$��29��$��Bottom=�$
��Left=�$��90��{�.  �� -   $ btnViewStatements��$��Estados de cuenta��$��	10,0,0,10q�$
 Hand��$��5��$��29��$��Bottom=�$
��Left=�$��112.667���� -   $ btnElegibility��$��Elegibilidad��$��317.667,0,0,10q�$
 Hand��$��5��$��29��$��Bottom=�$
��Left=�$��90��{�.  