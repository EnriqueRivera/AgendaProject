   M S B A M L   `   `   `  ����   MyDentApplication-    'MyDentApplication.FinishedEventsControl   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     >�Name$  UserControl��$��256.057��$��646.966��$��1q�
���   �.��� -    �Name$ lrFinishedEvents��.+�� -   $ gridBackground�� J� Opacity$	 0.6Q��� -    ��Name$ gbEventInfo��$��Información de la cita��$��	10,0,0,10q�$
��Left=�$	��251��$
��Bold"�$	��1.5q�
���   �.��� -   $ gridEventInfo��$��0,0,-2,0q�.+��� -    ��Name$ lblEventStartTimeCaption��$��Inició:��$��19.388,72,0,0q�$��Normal"�$
��Left=�$��41.134��$��25��$	��Top=��� -    ��Name$ lblEventStartTime��$�� ��$
��Left=�$��20��$��65.522,77,0,0q�$	��Top=�$��165.478��$��Normal"�$��Italic$��� -   $ lblEventEndTimeCaption��$��	Terminó:��$��5,97,0,0q�$��Normal"�$
��Left=�$��55.522��$��25��$	��Top=��� -	   $ lblEventEndTime��$�� ��$
��Left=�$��20��$��65.522,102,0,0q�$	��Top=�$��165.478��$��Normal"�$��Italic$��� -
   $ lblEventStatusCaption��$��Estado:��$��
10,122,0,0q�$��Normal"�$
��Left=�$��55.522��$��25��$	��Top=��� -   $ lblEventStatus��$�� ��$
��Left=�$��20��$��65.522,127,0,0q�$	��Top=�$��165.478��$��Normal"�$��Italic$��� -   $ lblEventCapturerCaption��$$��Usuario que registró la cita:��$��
10,149,0,0q�$��Normal"�$
��Left=�$��153.522��$��27��$	��Top=��� -   $ lblEventCapturer��$�� ��$
��Left=�$��20��$��
10,178,0,0q�$	��Top=�$	��221��$��Normal"�$��Italic$� �� TextAlignment$ Center=��� -   $ lblEventIdCaption��$��Cita #��$��19.388,22,0,0q�$��Normal"�$
��Left=�$��41.134��$��25��$	��Top=��� -   $ 
lblEventId��$�� ��$
��Left=�$��20��$��65.522,27,0,0q�$	��Top=�$��165.478��$��Normal"�$��Italic$��� -   $ lblEventDateCaption��$��Día:��$��31.388,47,0,0q�$��Normal"�$
��Left=�$��29.134��$��25��$	��Top=��� -   $ lblEventDate��$
��Left=�$��20��$��65.522,52,0,0q�$	��Top=�$��165.478��$��Normal"�$��Italic$��� -   $ gbPacientInfo��$��Información del paciente��$��
266,0,0,10q�$
��Bold"�$	��1.5q�$
��Left=�$	��231��
���   �.��� -   $ gridPacientInfo��$��Center=�$��216.115��$��0,1.991,15,-0.009q�.+��� -   $ lblExpNoCaption��$��Exp. No:��$��35.612,-1,0,0q�$��Normal"�$��25��$	��Top=�$
��Left=�$��55.522���� -   $ lblExpNo��$�� ��$��20��$��96.134,4,0,0q�$	��Top=�$��Normal"�$��Italic$��� -   $ lblCellPhoneCaption��$��Teléfono celular:��$��0,74,0,0q�$��Normal"�$
��Left=�$��97.134��$��25��$	��Top=��� -   $ lblCellPhone��$�� ��$��20��$��0,99,0,0q�$	��Top=�$��Normal"�$ Center=�$��Italic$��� -   $ lblHomePhoneCaption��$��Teléfono de casa:��$��	0,121,0,0q�$��Normal"�$
��Left=�$��101.134��$��25��$	��Top=��� -   $ lblHomePhone��$�� ��$��20��$��	0,146,0,0q�$	��Top=�$��Normal"�$ Center=�$��Italic$��� -   $ lblEmailCaption��$��Email:��$��	0,168,0,0q�$��Normal"�$
��Left=�$��41.134��$��25��$	��Top=��� -   $ lblEmail��$�� ��$��20��$��	0,193,0,0q�$	��Top=�$��Normal"�$ Center=�$��Italic$��� -   $ lblPacientNameCaption��$��Nombre completo:��$��0,27,0,0q�$��Normal"�$
��Left=�$	��111��$��25��$	��Top=��� -   $ lblPacientName��$�� ��$��20��$��0,52,0,0q�$	��Top=�$��Normal"�$ Center=�$��Italic$��� -    ��Name$ btnPatientNotSkips��$��Asistió el paciente��$
��Left=�$��35.549��$��509,56.451,0,0q�$	��Top=�$��127.966�� � Cursor$
 Hand��
	 � Tag$	 	COMPLETED���� -   $ btnPatientSkips��$��No asistió el paciente��$
��Left=�$��35.549��$��509,108.451,0,0q�$	��Top=�$��127.966��
 J� RenderTransformOrigin$
 
0.383,0.55&�$
 Hand��$	 PATIENT_SKIPS���� -    $ btnPatientCancel��$��Paciente canceló��$
��Left=�$��35.549��$��509,159.451,0,0q�$	��Top=�$��127.966��$
 
0.383,0.55&�$
 Hand��$	 CANCELED��