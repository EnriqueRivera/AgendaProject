   M S B A M L   `   `   `  ����L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 60   ,    &MyDentApplication.ManageContactsWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      1localclr-namespace:MyDentApplication     5      9System&clr-namespace:System;assembly=mscorlib     60   -   5          �Name$  Window��5       � Title$ Administrar contactos��5      $��696.716��5      $	��480��6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6    � WindowStartupLocation$ CenterScreen=�6W   .��5	      � -   5         �Name$ 
LayoutRoot��5	      .+�5
      &   System.Windows.Controls.DataGrid  -   5          Name$ 
dgContacts��5
      $��10,52,10,46q�6!      
IsReadOnly .    CanUserSortColumns .    AutoGenerateColumns .  	   SelectionMode$	 Single=�5      
   CanUserAddRows
 .     CanUserDeleteRows .  $��1��5      ��5      �� *ObservableData+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Nombre(s)��6      Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.11* 6J   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6,   �� *	FirstName+6J     5      $ Apellido(s)��6   $ 0.11* 6K    6.   �� *
LastName+6K     5      $ 
Dirección��6   $
 0.2* 6H    6,   �� *	Address+6H     5      $ Teléfono celular��6   $ 0.11* 6Q    63   �� *	CellPhone+6Q     5      $ Teléfono de casa��6   $ 0.11* 6Q    63   �� *	HomePhone+6Q   5      �� 5      -   5         ��Name$ btnDeleteContact��5      $��Eliminar��6%   $��	0,0,10,10q�68    � Cursor$
 Hand��6K   $��5��6Y   $��29��6f   $��Bottom=�6r   $��Right=�6�   $��60��6�   6�   �� 5      -   5        $ btnEditContact��5      $��Editar��6#   $��	0,0,75,10q�64   $
 Hand��6G   $��4��6U   $��Right=�6b   $��60��6~   $��29��6�   $��Bottom=�6�   6�   �� 5      -   5        $ btnAddContact��5      $��Agregar��6"   $��Right=�64   $��
0,0,140,10q�6P   $��60��6d   $
 Hand��6o   $��3��6}   $��29��6�   $��Bottom=�6�   6�   �� 5      -   5        $ btnViewAllContacts��5      $��Ver todo��6'   $��25��6:   $��0,14.5,10,0q�6F   $	��Top=�6[   $��Right=�6�   $��63.333��6�   $
 Hand��6�   �� 5      -   5         ��Name$ cbFilter��5      $��26��6   $��84,16.02,0,0q�6+   $	��Top=�6A   $��Normal"�6Y   $��1��6m   $
��Left=�6z   $��100.5��6�    �� SelectedIndex$ 0��6�   .@�5      ��	Nombre(s)��6   ��5      Apellido(s)��6    ��5      
Dirección��6   5      �� 5      $��69��6
   $	��Top=�6   $��10,14.02,0,0q�6-   $��27��6C   $
��Left=�6O   $��Buscar por:��6j   �� 5       -	   5         ��Name$ txtSearchTerm��5       $
��Left=�6#   $��25.02��6>   $��
192,16,0,0q�6M    �� TextWrapping$
 Wrap=�6a   $	��Top=�6u   $	��166��6�   
���   �6�   �� 5!      -
   5        $ btnRefreshContacts��5!      $��
363,16,0,0q�6'   $
 Hand��6;   $
��Left=�6I   $��25��6d   $��25��6o   $	��Top=�6{   $��2��6�   .��5"      �� $:��4/MyDentApplication;component/Images/refresh_icon.png��6   $
��Fill=�6I   $��20��6X   5#      5$      5%      