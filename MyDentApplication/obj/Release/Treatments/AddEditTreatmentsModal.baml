   M S B A M L   `   `   `  ����   MyDentApplication.    (MyDentApplication.AddEditTreatmentsModal   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     �Name$  Window�� � Title$% Agregar tratamiento para agenda��$	��490��$��565.5�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=� � Icon$= 7/MyDentApplication;component/Configuration/app_icon.ico��.��� -    �Name$ 
LayoutRoot��.+��� -    ��Name$ btnAddUpdateTreatment��$��Agregar��$��
0,0,106,10q� � Cursor$
 Hand��$��7��$��29��$��Bottom=�$��Right=�$��91���� -   $ 	btnCancel��$��Cancelar��$��	0,0,10,10q�$
 Hand��$��8��$��29��$��Bottom=�$��Right=�$��91���� -    ��Name$ lblTreatmentName��$��Nombre del tratamiento:��$
��Left=�$��27��$��	10,10,0,0q�$	��Top=�$	��344���� -   	 ��Name$	 txtTreatmentName��$��27��$��
10,37,10,0q�
 �� TextWrapping$

 Wrap=�$	��Top=�$��1��$��1q�
���   � �� TextAlignment$ Center=��� -   $ lblTreatmentDuration��$��
Duración:��$
��Left=�$��27��$��	10,80,0,0q�$	��Top=�$	��344���� -    ��Name$ cbTreatmentDuration��$
��Left=�$��28��$��
10,107,0,0q�$	��Top=�$	��100��$��Center=�$��Right=�$��2���� -	   $ lblMinutesCaption��$��minutos��$
��Left=�$��27��$��115,108,0,0q�$	��Top=�$	��239���� -
   $ lblIsRecurrent��$��Recurrente:��$
��Left=�$��27��$��38.042,143.382,0,0q�$	��Top=�$��71.958���� -    ��Name$ chkIsRecurrent��$�� ��$
��Left=�$��18��$��115,149.882,0,0q�$	��Top=�$��15.333��$��3���� -   $ lblRecurrentDays��$��Asistir dentro de:��$
��Left=�$��27��$��10,175.382,0,0q�$	��Top=�$	��100���� -   $	 txtRecurrentDays��$
��Left=�$��27��$��115,175.382,0,0q�$

 Wrap=�$	��Top=�$��108.5��$��4��$��1q�
���   �{�.  $��Right=��� -   $ lblRecurrentDaysText��$��días��$
��Left=�$��27��$��228.5,175.382,0,0q�$	��Top=�$��135.5���� -   $ lblIsRecurrent_Copy��$��Correo de inasistencia:��$
��Left=�$��27��$��10,221.382,0,0q�$	��Top=�$	��130���� -   $ chkIsAbsence��$�� ��$
��Left=�$��18��$��145,226.718,0,0q�$	��Top=�$��15.333��$��5���� -   $ lblRecurrentDays_Copy��$��Mensaje:��$
��Left=�$��27��$��10,253.382,0,0q�$	��Top=�$��60���� -   $	 txtAbsenceMessage��$��10,280.382,10,50q�" �� VerticalScrollBarVisibility$
 Auto=�$

 Wrap=� �� AcceptsReturn . 
���   �$��6��{�.  