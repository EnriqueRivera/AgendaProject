   M S B A M L   `   `   `  ����  MyDentApplication-    'MyDentApplication.AddEditRemindersModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5          �Name$  Window��5       � Title$ Crear recordatorio��5      $	��355��5      $	��355��6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6    � WindowStartupLocation$ CenterScreen=�6S   $	��355��6x   $	��355��6�   .��5
      � -   5         �Name$ 
LayoutRoot��5
      .+�5      �� -   5         ��Name$ lblReminderMessage��5   
   $��Mensaje:��6&   $
��Left=�69   $��25.672��6T   $��	10,10,0,0q�6d   $	��Top=�6w   $��60.408��6�   �� 5      $��1q�6   
���   �$��10,40.672,10,0q�63   $��118.835��6K   $	��Top=�6\   ._�5      �� -   5         ��Name$ txtReminderMessage��5      $��0,0,0,0q�6)   $��Italic$�6:   " �� VerticalScrollBarVisibility$
 Auto=�6M    �� TextWrapping$
 Wrap=�6p   	 �� AcceptsReturn	 . 6�   5      �� 5      -   5        $ lblReminderMessage_Copy��5   
   $!��Fecha y hora de aparición:��6+   $
��Left=�6P   $��25.672��6k   $��11,176.507,0,0q�6{   $	��Top=�6�   $��149.741��6�   &   Xceed.Wpf.Toolkit.DateTimePicker  5      -   5        
  Name$
 dtpReminderAppearDate��5      $��26��67   $��10,207.179,10,0q�6C   $	��Top=�6\   
���   �,  &Xceed.Wpf.Toolkit.Primitives.InputBase   CultureInfo$ es-MX��6�   �� 5      -   5         ��Name$ chkRequireAdmin��5      $��Requiere aprobación��6&   $
��Left=�6D   $��11,253.507,0,0q�6_   $	��Top=�6w   �� 5      -   5         ��Name$ btnAddUpdateReminder��5      $��Agregar��6)   $��
0,0,106,10q�6;    � Cursor$
 Hand��6O   $��6��6]   $��29��6j   $��Bottom=�6v   $��Right=�6�   $��91��6�   6�   �� 5      -	   5        $ 	btnCancel��5      $��Cancelar��6   $��	0,0,10,10q�61   $
 Hand��6D   $��7��6R   $��29��6_   $��Bottom=�6k   $��Right=�6�   $��91��6�   6�   5      5      