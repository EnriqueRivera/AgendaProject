   M S B A M L   `   `   `  ����   MyDentApplication%    MyDentApplication.AddEventModal   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     �Name$  Window�� � Title$ Agendar cita��$	��582��$	��282�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ CanMinimize=� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��.��� -    �Name$ 
LayoutRoot��.+��� -    ��Name$ 	gbPatient��$��Datos del paciente��$
��Left=�$��	10,10,0,0q�$	��Top=�$	��193��$��288.697��$
��Bold"�.��� -   $ gridPatient��$��	0,0,-2,-2q�.+��� -    ��Name$ cbPatientName��$��26��$��71.447,9.96,10,0q�$	��Top=�$��Normal"�$��1���� -    ��Name$ lblPatientNameCaption��$��Nombre:��$
��Left=�$��	10,10,0,0q�$	��Top=�$��Normal"�$��25���� -   $ lblExpNoCaption��$��Exp. No.��$
��Left=�$��53.82,48,0,0q�$	��Top=�$��Normal"�$��25���� -   	 ��Name$	 lblExpNo��$�� ��$
��Left=�$��111.943,53,0,0q�$	��Top=�$��Normal"�$��20��$��156.754���� -	   $ lblCellphoneCaption��$��Teléfono celular:��$
��Left=�$��	10,78,0,0q�$	��Top=�$��Normal"�$��25���� -
   $	 lblCellphone��$�� ��$
��Left=�$��111.943,83,0,0q�$	��Top=�$��Normal"�$��20��$��156.754���� -   $ lblHomePhoneCaption��$��Teléfono de casa:��$
��Left=�$��5.223,108,0,0q�$	��Top=�$��Normal"�$��25���� -   $	 lblHomePhone��$�� ��$
��Left=�$��111.943,113,0,0q�$	��Top=�$��Normal"�$��20��$��156.754���� -   $ lblEmailCaption��$��Email:��$
��Left=�$��66.016,138,0,0q�$	��Top=�$��Normal"�$��25���� -   $	 lblEmail��$�� ��$
��Left=�$��111.943,142.04,0,0q�$	��Top=�$��Normal"�$��20��$��156.754���� -   $ 
gbTratment��$��Tratamiento a realizar��$
��Left=�$��303.697,10,0,0q�$	��Top=�$��94��$��252.303��$
��Bold"�.��� -   $ gridTratment��$��	0,0,-2,-2q�.+��� -   $ cbTratmentName��$��79.553,9.96,10,0q�$��26��$	��Top=�$��Normal"�$��2���� -   $ lblTratmentNameCaption��$��Nombre:��$��12.106,10,0,0q�$��Normal"�$
��Left=�$��62.447��$��25��$	��Top=��� -   $ lblDurationCaption��$��
Duración:��$
��Left=�$��25��$��10,45.04,0,0q�$	��Top=�$��66��$��Normal"��� -   $	 lblDuration��$�� ��$
��Left=�$��20��$��79.553,50.04,0,0q�$	��Top=�$��160.144��$��Normal"��� -   
 ��Name$
 	btnAccept��$��Aceptar��$
��Left=�$��25��$��411,212,0,0q�$	��Top=�$��70�� � Cursor$
 Hand��$��3���� -   $
 	btnCancel��$��Cancelar��$
��Left=�$��25��$��486,212,0,0q�$	��Top=�$��70��$
 Hand��$��4���� -   $ gbEventTime��$��Hora de la cita��$
��Left=�$��303.697,109,0,0q�$	��Top=�$��94��$��252.303��$
��Bold"�.��� -   $ gridEventTime��$��	0,0,-2,-2q�.+��� -   $ lblEventStartTimeCaption��$��Inicia:��$��	27,10,0,0q�$��Normal"�$
��Left=�$��40.447��$��25��$	��Top=��� -   $	 lblEventStartTime��$�� ��$
��Left=�$��20��$��72.447,15,0,0q�$	��Top=�$��165.144��$��Normal"��� -   $ lblEventEndTimeCaption��$��Termina:��$��	10,40,0,0q�$��Normal"�$
��Left=�$��57.447��$��25��$	��Top=��� -   $	 lblEventEndTime��$�� ��$
��Left=�$��20��$��72.447,45,0,0q�$	��Top=�$��165.144��$��Normal"��� -   $# lblUpdateClinicHistoryMessage��$E��?El paciente seleccionado debe actualizar su historial clínico.��$
��Left=�$��32��$��10,0,0,2q�$��Bottom=�$��349.934��
���  ��$
��Bold"�$y�Hidden=�