   M S B A M L   `   `   `  ����  MyDentApplication.    (MyDentApplication.AddEditTreatmentsModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      -   5          �Name$  Window��5       � Title$% Agregar tratamiento para agenda��5      $	��490��5      $��565.5��6    � 
ResizeMode$ CanMinimize=�6    � WindowStartupLocation$ CenterScreen=�66    � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico��6[   .��5	      � -   5         �Name$ 
LayoutRoot��5	      .+�5
      �� -   5         ��Name$ btnAddUpdateTreatment��5
      $��Agregar��6*   $��
0,0,106,10q�6<    � Cursor$
 Hand��6P   $��7��6^   $��29��6k   $��Bottom=�6w   $��Right=�6�   $��91��6�   �� 5      -   5        $ 	btnCancel��5      $��Cancelar��6   $��	0,0,10,10q�61   $
 Hand��6D   $��8��6R   $��29��6_   $��Bottom=�6k   $��Right=�6�   $��91��6�   �� 5      -   5         ��Name$ lblTreatmentName��5   
   $��Nombre del tratamiento:��6$   $
��Left=�6F   $��27��6a   $��	10,10,0,0q�6m   $	��Top=�6�   $	��344��6�   �� 5      -   5        	 ��Name$	 txtTreatmentName��5      $��27��6&   $��
10,37,10,0q�62   
 �� TextWrapping$

 Wrap=�6F   $	��Top=�6Z   $��1��6r   $��1q�6   
���   � �� TextAlignment$ Center=�6�   �� 5      -   5        $ lblTreatmentDuration��5   
   $��
Duración:��6(   $
��Left=�6<   $��27��6W   $��	10,80,0,0q�6c   $	��Top=�6v   $	��344��6�   �� 5      -   5         ��Name$ cbTreatmentDuration��5      $
��Left=�6*   $��28��6E   $��
10,107,0,0q�6Q   $	��Top=�6e   $	��100��6}   $��Center=�6�   $��Right=�6�   $��2��6�   �� 5      -	   5        $ lblMinutesCaption��5   
   $��minutos��6%   $
��Left=�67   $��27��6R   $��115,108,0,0q�6^   $	��Top=�6s   $	��239��6�   �� 5      -
   5        $ lblIsRecurrent��5   
   $��Recurrente:��6"   $
��Left=�68   $��27��6S   $��38.042,143.382,0,0q�6_   $	��Top=�6{   $��71.958��6�   �� 5      -   5         ��Name$ chkIsRecurrent��5      $�� ��6%   $
��Left=�60   $��18��6K   $��115,149.882,0,0q�6W   $	��Top=�6p   $��15.333��6�   $��3��6�   �� 5      -   5        $ lblRecurrentDays��5   
   $��Asistir dentro de:��6$   $
��Left=�6A   $��27��6\   $��10,175.382,0,0q�6h   $	��Top=�6�   $	��100��6�   �� 5      -   5        $	 txtRecurrentDays��5      $
��Left=�6&   $��27��6A   $��115,175.382,0,0q�6M   $

 Wrap=�6f   $	��Top=�6z   $��108.5��6�   $��4��6�   $��1q�6�   
���   �{�.  $��Right=�6�   �� 5      -   5        $ lblRecurrentDaysText��5   
   $��días��6(   $
��Left=�67   $��27��6R   $��228.5,175.382,0,0q�6^   $	��Top=�6y   $��135.5��6�   �� 5      -   5        $ lblIsRecurrent_Copy��5   
   $��Correo de inasistencia:��6'   $
��Left=�6I   $��27��6d   $��10,221.382,0,0q�6p   $	��Top=�6�   $	��130��6�   �� 5      -   5        $ chkIsAbsence��5      $�� ��6#   $
��Left=�6.   $��18��6I   $��145,226.718,0,0q�6U   $	��Top=�6n   $��15.333��6�   $��5��6�   �� 5      -   5        $ lblRecurrentDays_Copy��5   
   $��Mensaje:��6)   $
��Left=�6<   $��27��6W   $��10,253.382,0,0q�6c   $	��Top=�6{   $��60��6�   �� 5      -   5        $	 txtAbsenceMessage��5      $��10,280.382,10,50q�6'   " �� VerticalScrollBarVisibility$
 Auto=�6A   $

 Wrap=�6d    �� AcceptsReturn . 
���   �$��6��6�   {�.  6�   5      5      