   M S B A M L   `   `   `  ���� G  CWpfScheduler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:WpfScheduler;assembly=WpfSchedulerWpfScheduler   MyDentApplication$   MyDentApplication.AgendaWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44wpfx)http://schemas.xceed.com/wpf/xaml/toolkit  CWpfScheduler0clr-namespace:WpfScheduler;assembly=WpfScheduler  4xctk)http://schemas.xceed.com/wpf/xaml/toolkit    � Title$  Agenda��$	��800��$��823.5��$	��600��$	��600�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � WindowState$ 	Maximized=� � WindowStartupLocation$ CenterScreen=�.��� .+�   WpfScheduler.Scheduler  -     Name$ 	scheduler��$��Stretch=�$��Stretch=�$��250,10,10,39.666q�&   System.Windows.Controls.Calendar  -     Name$ calendar��$
��Left=�$��35,32.999,0,0q�$	��Top=��� -    ��Name$ gbFilterEvents��$��Filtrar citas��$��10,483.742,0,39.666q�$
��Bold"�$
��Left=�$	��235��$��3q�
���   �.���� $��0,0,-2,1.132q�.��� $��180.557��$	��225��.+��� -    ��Name$ lblCanceledEvents��$��
Canceladas��$
��Left=�$��84.5,9.999,0,0q�$	��Top=�$��Normal"��� -    ��Name$ chkCanceledEvents��$��CheckBox��$
��Left=�$��15��$��9,14.999,0,0q�$	��Top=�$��15��
	 � Tag$	 1��
 d� 	IsChecked$

 TrueY�$��2���� -   $ lblExceptionEvents��$��Con excepción��$
��Left=�$��84.5,36.666,0,0q�$	��Top=�$��Normal"��� -   $ chkExceptionEvents��$��CheckBox��$
��Left=�$��15��$��9,41.666,0,0q�$	��Top=�$��15��$

 TrueY�$	 2��$��4���� -   $ lblPatientSkipsEvents��$��Paciente no asistió��$
��Left=�$��84.5,64.666,0,0q�$	��Top=�$��Normal"��� -	   $ chkPatientSkipsEvents��$��CheckBox��$
��Left=�$��15��$��9,69.666,0,0q�$	��Top=�$��15��$

 TrueY�$	 3��$��6���� -
   $ lblCompletedEvents��$��Completadas��$
��Left=�$��84.086,118.757,0,0q�$	��Top=�$��Normal"��� -   $ chkCompletedEvents��$��CheckBox��$
��Left=�$��15��$��9,123.757,0,0q�$	��Top=�$��15��$

 TrueY�$	 5��$��10���� -   $ lblNotCompletedEvents��$��Sin concretar��$
��Left=�$��84.5,92.014,0,0q�$	��Top=�$��Normal"��� -   $ chkPendingEvents��$��CheckBox��$
��Left=�$��15��$��9,96.964,0,0q�$	��Top=�$��15��$

 TrueY�$	 4��$��8��#  Xceed.Wpf.Toolkit.ColorPicker  -     Name$ cpCanceledEvents��$
��Left=�$��21.667��$��29,11.999,0,0q�$	��Top=�$
��50.5�� � Cursor$
 Hand��   ShowAdvancedButton .     AvailableColorsHeader$ Colores disponibles��   RecentColorsHeader$ Colores recientes��   StandardColorsHeader$ Colores estándar��$	 CANCELED��$��3��  -   $ cpExceptionEvents��$
��Left=�$��21.667��$��29.527,38.459,0,0q�$	��Top=�$��49.973��$
 Hand�� .  $ Colores disponibles��$ Colores recientes��$ Colores estándar��$	 	EXCEPTION��$��5��  -   $ cpPatientSkipsEvents��$
��Left=�$��21.667��$��29,66.499,0,0q�$	��Top=�$
��50.5��$
 Hand�� .  $ Colores disponibles��$ Colores recientes��$ Colores estándar��$	 PATIENT_SKIPS��$��7��  -   $ cpPendingEvents��$
��Left=�$��21.667��$��29,94.014,0,0q�$	��Top=�$
��50.5��$
 Hand�� .  $ Colores disponibles��$ Colores recientes��$ Colores estándar��$	 PENDING��$��9��  -   $ cpCompletedEvents��$
��Left=�$��21.667��$��29.527,120.681,0,0q�$	��Top=�$��49.559��$
 Hand�� .  $ Colores disponibles��$ Colores recientes��$ Colores estándar��$	 	COMPLETED��$��11���� -   $ lblCompletedEvents_Copy��$��Confirmadas��$
��Left=�$��84.5,145.424,0,0q�$	��Top=�$��Normal"��� -   $ chkConfirmedEvents��$��CheckBox��$
��Left=�$��15��$��8.5,150.424,0,0q�$	��Top=�$��15��$

 TrueY�$	 6��$��10��  -   $ cpConfirmedEvents��$
��Left=�$��21.667��$��29.941,147.348,0,0q�$	��Top=�$��49.559��$
 Hand�� .  $ Colores disponibles��$ Colores recientes��$ Colores estándar��$	 	CONFIRMED��$��11���� -   $ gbPacientInfo��$��Información del paciente��$��10,337.334,0,0q�$
��Bold"�$��3q�
���   �$
��Left=�$	��235��$��141.408��$	��Top=�.���� -    ��Name$ svPacientInfo��$��0,0,-2,0.667q�.��� -    �Name$ gridPacientInfo��$��Center=�$��216.114��.+��� -   $ lblExpNoCaption��$��Exp. No:��$��35.612,-1,0,0q�$��Normal"�$��25��$	��Top=�$
��Left=�$��55.522���� -    ��Name$ lblExpNo��$�� ��$��20��$��96.134,4,0,0q�$	��Top=�$��Normal"�$��Italic$��� -   $ lblCellPhoneCaption��$��Teléfono celular:��$��0,74,0,0q�$��Normal"�$
��Left=�$��97.134��$��25��$	��Top=��� -   $ lblCellPhone��$�� ��$��20��$��0,99,0,0q�$	��Top=�$��Normal"� �� TextAlignment$ Center=�$��Italic$��� -   $ lblHomePhoneCaption��$��Teléfono de casa:��$��	0,121,0,0q�$��Normal"�$
��Left=�$��101.134��$��25��$	��Top=��� -   $ lblHomePhone��$�� ��$��20��$��	0,146,0,0q�$	��Top=�$��Normal"�$ Center=�$��Italic$��� -   $ lblEmailCaption��$��Email:��$��	0,168,0,0q�$��Normal"�$
��Left=�$��41.134��$��25��$	��Top=��� -    $ lblEmail��$�� ��$��20��$��0,193,0,-12.886q�$	��Top=�$��Normal"�$ Center=�$��Italic$��� -!   $ lblPacientNameCaption��$��Nombre completo:��$��0,27,0,0q�$��Normal"�$
��Left=�$	��111��$��25��$	��Top=��� -"   $ lblPacientName��$�� ��$��20��$��0,52,0,0q�$	��Top=�$��Normal"�$ Center=�$��Italic$��� -#   $ gbEventInfo��$��Información de la cita��$��10,205.966,0,0q�$
��Left=�$	��235��$
��Bold"�$��3q�
���   �$��126.368��$	��Top=�.���� -$   $ svEventInfo��$��0,0,-2,0.741q�.��� -%   $ gridEventInfo��.+��� -&   $ lblEventStartTimeCaption��$��Inicia:��$��19.388,27,0,0q�$��Normal"�$
��Left=�$��41.134��$��25��$	��Top=��� -'   $ lblEventStartTime��$�� ��$
��Left=�$��20��$��65.522,32,0,0q�$	��Top=�$��137.978��$��Normal"�$��Italic$��� -(   $ lblEventEndTimeCaption��$��Termina:��$��5,52,0,0q�$��Normal"�$
��Left=�$��55.522��$��25��$	��Top=��� -)   $ lblEventEndTime��$�� ��$
��Left=�$��20��$��65.522,57,0,0q�$	��Top=�$��137.978��$��Normal"�$��Italic$��� -*   $ lblEventStatusCaption��$��Estado:��$��	10,77,0,0q�$��Normal"�$
��Left=�$��55.522��$��25��$	��Top=��� -+   $ lblEventStatus��$�� ��$
��Left=�$��20��$��65.522,82,0,0q�$	��Top=�$��137.978��$��Normal"�$��Italic$��� -,   $ lblEventCapturerCaption��$$��Usuario que registró la cita:��$��
10,104,0,0q�$��Normal"�$
��Left=�$��153.522��$��27��$	��Top=��� --   $ lblEventCapturer��$�� ��$
��Left=�$��20��$��10,133,0,-10.592q�$	��Top=�$��193.5��$��Normal"�$��Italic$�$ Center=��� -.   $ lblEventIdCaption��$��Cita #��$��19.388,2,0,0q�$��Normal"�$
��Left=�$��41.134��$��25��$	��Top=��� -/   $ 
lblEventId��$�� ��$
��Left=�$��20��$��65.522,7,0,0q�$	��Top=�$��137.978��$��Normal"�$��Italic$��� -0   $! lblEventEndTimeCaption_Copy��$��Reagendada:��$��
10,158,0,0q�$��Normal"�$
��Left=�$��80.855��$��26��$	��Top=��� -1   $ lblNextEvent��$�� ��$
��Left=�$��20��$��
10,185,0,0q�$	��Top=�$��193.5��$��Normal"�$��Italic$�$ Center=��� -2    ��Name$ btnToday��$	��Hoy��$
��Left=�$��22.999��$��	35,10,0,0q�$	��Top=�$	��179��$
 Hand��$��1���� -3   $ lblLoggedInCaption��$��Sesión iniciada como:��$��0,0,169.5,10q�$��Right=�$��123.5��$��24��$��Bottom=��� -4   $ lblLoggedIn��$�� ��$��0,0,10,7.999q�$��Italic$�$��Right=�$��159.5��$��26��$��Bottom=��� -5   $# lblUpdateClinicHistoryMessage��$P��JEl paciente agendado para esta cita debe actualizar su historial clínico.��$
��Left=�$��26.667��$��10,0,0,7.999q�$��Bottom=�$	��413��
���  ��$
��Bold"�$y�Hidden=�