   M S B A M L   `   `   `  ����  MyDentApplication,    &MyDentApplication.AddEditContactsModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5          �Name$  Window��5       � Title$ Agregar contacto��5      $	��380��5      $	��347��6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6    � 
ResizeMode$ CanMinimize=�6S    � WindowStartupLocation$ CenterScreen=�6l   .��5	      � -   5         �Name$ 
LayoutRoot��5	      $��0,0,2,2q�6   .+�5
      �� -   5         ��Name$ lblMedicineNotes��5
   
   $��Dirección:��6$   $
��Left=�69   $��25.672��6T   $��10,142.657,0,0q�6d   $	��Top=�6|   $��95.408��6�   �� 5      $��1q�6   
���   �$��10,168.329,10,52.667q�63   ._�5      �� -   5         ��Name$ txtContactAddress��5      $��0,0,0,0q�6(   $��Italic$�69   " �� VerticalScrollBarVisibility$
 Auto=�6L   	 �� TextWrapping$
	 Wrap=�6o   $��5��6�   5      �� 5      -   5        $ lblContactFirstName��5   
   $��
Nombre(s):��6'   $
��Left=�6<   $��27��6W   $��46.5,10,0,0q�6c   $	��Top=�6x   $��68��6�   �� 5      -   5        $ txtContactFirstName��5      $��27��6)   $��119.5,10,10,0q�65   $
	 Wrap=�6L   $	��Top=�6`   $��1��6x   $��1q�6�   
���   �6�   �� 5      -   5        $ lblContactLastName��5   
   $��Apellido(s):��6&   $
��Left=�6=   $��27��6X   $��46.5,42,0,0q�6d   $	��Top=�6y   $��68��6�   �� 5      -   5        $ lblContactCellPhone��5   
   $��Teléfono celular:��6'   $
��Left=�6C   $��27��6^   $��16.333,74,0,0q�6j   $	��Top=�6�   $��98.167��6�   �� 5      -	   5        $ txtContactLastName��5      $��27��6(   $��119.5,42,10,0q�64   $
	 Wrap=�6K   $	��Top=�6_   $��2��6w   $��1q�6�   
���   �
 K� ZIndex$
 1��6�   �� 5      -
   5        $ txtContactCellPhone��5      $��27��6)   $��119.5,74,10,0q�65   $
	 Wrap=�6L   $	��Top=�6`   $��3��6x   $��1q�6�   
���   �$
 3��6�   �� 5      -   5         ��Name$ btnAddUpdateContact��5      $��Agregar��6(   $��Right=�6:   $��
0,0,106,10q�6V   $��91��6j    � Cursor$
 Hand��6�   $��6��6�   $��29��6�   $��Bottom=�6�   $
 6��6�   �� 5      -   5        $ 	btnCancel��5      $��Cancelar��6   $��Right=�61   $��	0,0,10,10q�6M   $��91��6`   $
 Hand��6�   $��7��6�   $��29��6�   $��Bottom=�6�   $
 7��6�   �� 5      -   5        $ lblContactHomePhone��5   
   $��Teléfono de casa:��6'   $
��Left=�6C   $��27��6^   $��
11,106,0,0q�6j   $	��Top=�6~   $��103.5��6�   �� 5      -   5        $ txtContactHomePhone��5      $��27��6)   $��119.5,106,10,0q�65   $
	 Wrap=�6M   $	��Top=�6a   $��4��6y   $��1q�6�   
���   �$
 3��6�   5      5      