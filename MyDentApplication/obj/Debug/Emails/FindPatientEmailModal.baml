   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5       MyDentApplication-   'MyDentApplication.FindPatientEmailModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      9System&clr-namespace:System;assembly=mscorlib     5      -   5          �Name$  Window��5       � Title$ Seleccionar paciente��5      $	��652��5      $��504.955��6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6    � WindowStartupLocation$ CenterScreen=�6W   .��5
      � -   5         �Name$ 
LayoutRoot��5
      .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ 
dgPatients��5      $��10,58,10,44q�6!      
IsReadOnly .    CanUserSortColumns .    AutoGenerateColumns .  	   CanUserAddRows	 .  
   CanUserDeleteRows
 .  $��4��5      ��5      �� *ObservableData+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Exp. No.��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.07* 6V   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *
AssignedId+6V     5      $ 	Nombre(s)��6   $ 0.15* 6J    6,   �� *	FirstName+6J     5      $ Apellido(s)��6   $ 0.15* 6K    6.   �� *
LastName+6K     5      $ Email��6   $ 0.15* 6B    6(   �� *Email+6B   5      �� 5      -   5         ��Name$ 	btnAccept��5      $��Aceptar��6   $��	0,0,75,10q�60    � Cursor$
 Hand��6C   $��5��6Q   $��29��6^   $��Bottom=�6j   $��Right=�6�   $��60��6�   6�   �� 5      -   5        $ 	btnCancel��5      $��Cancelar��6   $��	0,0,10,10q�61   $
 Hand��6D   $��6��6R   $��29��6_   $��Bottom=�6k   $��Right=�6�   $��60��6�   6�   �� 5      -   5        $ btnViewAllPatients��5      $��63.333��6'   $	��Top=�66   $��0,14.5,10,0q�6N   $��25��6c   $��Right=�6o   $��Ver todo��6�   $
 Hand��6�   $��3��6�   �� 5      -   5         ��Name$ cbFilter��5      $��26��6   $��84,16.02,0,0q�6+   $	��Top=�6A   $��Normal"�6Y   $��1��6m   $
��Left=�6z   $��100.5��6�    �� SelectedIndex$ 0��6�   .@�5      ��	Nombre(s)��6   ��5      Apellido(s)��6    ��5      	Email��6   5      �� 5      $��69��6
   $	��Top=�6   $��10,14.02,0,0q�6-   $��27��6C   $
��Left=�6O   $��Buscar por:��6j   �� 5      -   5         ��Name$ txtSearchTerm��5      $
��Left=�6#   $��25.02��6>   $��
192,16,0,0q�6M    �� TextWrapping$
 Wrap=�6a   $	��Top=�6u   $	��166��6�   
���   �$��1��6�   �� 5      -	   5        $ btnRefreshPatients��5      $��
363,16,0,0q�6'   $
 Hand��6;   $
��Left=�6I   $��25��6d   $��25��6o   $	��Top=�6{   $��2��6�   .��5       �� $:��4/MyDentApplication;component/Images/refresh_icon.png��6   $
��Fill=�6I   $��20��6X   5!      5"      5#      