   M S B A M L   `   `   `  ����  MyDentApplication)    #MyDentApplication.AddEditUsersModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      -   5          �Name$  Window��5       � Title$ Agregar usuario��5      $��374.167��5      $	��267��6    � 
ResizeMode$ CanMinimize=�6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��68    � WindowStartupLocation$ CenterScreen=�6p   .��5	      � -   5         �Name$ 
LayoutRoot��5	      .+�5
      �� -   5         ��Name$ 	lblUserId��5
   
   $��No. de usuario:��6   $
��Left=�67   $��27��6R   $��68.667,13,0,0q�6^   $	��Top=�6u   $��89.333��6�   �� 5      -   5        $ lblUserFirstName��5   
   $��
Nombre(s):��6$   $
��Left=�69   $��27��6T   $��88.667,45,0,0q�6`   $	��Top=�6w   $��69.333��6�   �� 5      -   5        $ lblUserLastName��5   
   $��Apellido(s):��6#   $
��Left=�6:   $��27��6U   $��88.667,77,0,0q�6a   $	��Top=�6x   $��69.333��6�   �� 5      -   5         ��Name$ 	txtUserId��5      $
��Left=�6   $��27��6:   $��167.5,13,0,0q�6F    �� TextWrapping$
 Wrap=�6\   $	��Top=�6p   $	��180��6�   $��1��6�   $��1q�6�   
���   �6�   �� 5      -   5        $ txtUserFirstName��5      $
��Left=�6&   $��27��6A   $��167.5,45,0,0q�6M   $
 Wrap=�6c   $	��Top=�6w   $	��180��6�   $��2��6�   
���   �6�   �� 5      -   5        $ txtUserLastName��5      $
��Left=�6%   $��27��6@   $��167.5,77,0,0q�6L   $
 Wrap=�6b   $	��Top=�6v   $	��180��6�   $��3��6�   
���   �6�   �� 5      -	   5        	 ��Name$	 btnAddUpdateUser��5      $��Agregar��6%   $
��Left=�67   $��29��6R   $��91.67,187.667,0,0q�6^   $	��Top=�6y   $��91��6�   
 � Cursor$

 Hand��6�   $��6��6�   �� 5      -
   5        $	 	btnCancel��5      $��Cancelar��6   $
��Left=�61   $��29��6L   $��187.67,187.667,0,0q�6X   $	��Top=�6t   $��91��6�   $

 Hand��6�   $��7��6�   �� 5      -   5        $ lblPassword��5   
   $��Nueva contraseña:��6   $
��Left=�6;   $��27��6V   $��46.667,109,0,0q�6b   $	��Top=�6z   $��111.333��6�   G� 5      -   5         G�Name$ 
pbPassword��5      $
��Left=�6$   $��27��6?   $��167.5,109,0,0q�6K   $	��Top=�6b   $	��180��6z   $��4��6�   
���   �6�   �� 5      -   5        $ lblRepeatPassword��5   
   $ ��Repetir nueva contraseña:��6%   $
��Left=�6I   $��27��6d   $��
10,141,0,0q�6p   $	��Top=�6�   $	��148��6�   G� 5      -   5        $ pbRepeatPassword��5      $
��Left=�6*   $��27��6E   $��167.5,141,0,0q�6Q   $	��Top=�6h   $	��180��6�   $��5��6�   
���   �6�   5      5      