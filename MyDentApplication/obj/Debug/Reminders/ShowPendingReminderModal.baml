   M S B A M L   `   `   `  ����  MyDentApplication0    *MyDentApplication.ShowPendingReminderModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      -   5          �Name$  Window��5       � Title$ Recordatorio��5      $	��398��5      $	��312��6    � 
ResizeMode$ NoResize=�6    � WindowStyle$ 
ToolWindow=�61    � WindowStartupLocation$ CenterScreen=�6c   .��5	      � -   5         �Name$ 
LayoutRoot��5	      .+�5
      �� -   5         ��Name$ lblReminderMessage��5
   
   $��Mensaje:��6&   $��25.672��69   $��10,81.642,10,0q�6I   $	��Top=�6a   �� 5      $��1q�6   
���   �$��10,107.314,10,0q�63   $��124.328��6L   $	��Top=�6]   ._�5      �� -   5         ��Name$ txtReminderMessage��5      $��0,0,0,0q�6)   $��Italic$�6:   " �� VerticalScrollBarVisibility$
 Auto=�6M   	 �� TextWrapping$
	 Wrap=�6p   
 �� 
IsReadOnly
 . 6�   5      �� 5      $$��Fecha y hora del recordatorio:��6
   $��28��63   $��
10,10,10,0q�6?   $	��Top=�6S   �� 5      -   5        $ lblReminderTime��5   
   $�� ��6#   $��28��6.   $��
10,38,10,0q�6:   $	��Top=�6N   $��Right=�6f   $��Italic$�6�   $��1q�6�   
���   �6�   �� 5      -   5         ��Name$ 	btnAccept��5      $��Aceptar��6   $��	0,0,10,10q�60   $��30��6C   $��Bottom=�6O   $��Right=�6j   $��87��6�    � Cursor$
 Hand��6�   6�   �� 5      -   5        $ lblRequireAdmin��5   
   $6��0El administrador debe aprobar este recordatorio.��6#   $��	10,0,0,10q�6^   
���  ��$��Italic$�6�   $
��Left=�6�   $	��262��6�   $��28��6�   $��Bottom=�6�   5      5      