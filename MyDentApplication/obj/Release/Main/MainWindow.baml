   M S B A M L   `   `   `  ���� L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  "    MyDentApplication.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     1localclr-namespace:MyDentApplication     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$  Window�� � Title$ Ventana principal��$
��1214��$	��774�� � WindowStartupLocation$ CenterScreen=� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � WindowState$ 	Maximized=�.��� -    �Name$ 
LayoutRoot�� J� OpacityMask
 �����.+��� -    ��Name$ lblLoggedInCaption��$��Sesión iniciada como:��$��0,0,169.5,12.001q�$��Right=�$��123.5��$��24��$��Bottom=��� -   $ lblLoggedIn��$�� ��$��	0,0,10,10q�$��Italic$�$��Right=�$��159.5��$��26��$��Bottom=��� -    ��Name$ gbPacientInfo��$��Recordatorios��$��0,10,10,41.001q�$
��Bold"�$��2q�
���   �
���   �$��Right=�$	��222��.��� -   $ gridPacientInfo��$��0,10,-2,2.245q�.+��� -   	 ��Name$	 rcPendingReminderColor��$��20��$��0,7.332,184.66,0q�
���   �$	��Top=�$��Right=�$��20������ 
 �� EndPoint$
 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FF5AC3EA��� $ 	#FFE0E0E0�� � Offset$	 0.5Q�� $ 	#FF5AC3EA��$ 1Q��� -   $	 rcReminderSeenColor��$��20��$��0,39.385,184.66,0q�
���   �$	��Top=�$��Right=�$��20������ $
 0.5,1&�$ 0.5,0&�.��� $ 	#FF5AEA61��� $ 	#FFE0E0E0��$	 0.5Q�� $ 	#FF5AEA61��$ 1Q��� -	   $ lblPendingReminders��$��
Pendientes��$��27��$��0,4.05,10,0q�$	��Top=�$��Right=�$��174.66��$��Normal"��� -
   $ lblSeenReminders��$��	Mostrados��$��27��$��0,36.05,10,0q�$	��Top=�$��Right=�$��174.66��$��Normal"��� -    ��Name$ svReminders��$��12,68.05,10,32q�
���   �$��0q�$��Normal"�$
��Auto=�.���� -    ��Name$ spReminders���� -    ��Name$ btnChangePassword��$��139.5,0,0,10q�$��85�� � Cursor$
 Hand��$
��Left=�$��124.5��$��Bottom=�.����  �� Orientation$ Vertical=�.+��� $A��;/MyDentApplication;component/Images/changePassword_icon.png��$��50��$��50���� $��Cambiar contraseña�� �� TextAlignment$ Center=��� -   $ 	btnLogOut��$��	10,0,0,10q�$��85��$
 Hand��$
��Left=�$��124.5��$��Bottom=�.���� $ Vertical=�.+��� $9��3/MyDentApplication;component/Images/logout_icon.png��$��50��$��50���� $��Cerrar sesión��$ Center=��� -   $ gbAdministration��$��Administración��$
��Left=�$��243.5,8.55,0,103.451q�$	��151��$
��Bold"�
���   �$��2q�
���   �.���� $��0,10,0,2.019q�$
��Auto=�$��Normal"�$��Center=�.���� .+��� -   $ btnManageUsers��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $8��2/MyDentApplication;component/Images/users_icon.png��$��50��$��50���� $��Usuarios��$ Center=��� -   $ btnViewPayments��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $;��5/MyDentApplication;component/Images/payments_icon.png��$��50��$��50���� $ Center=��� $�Historial de���� �� $�pagos���� -   $ btnManageTechnicals��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $<��6/MyDentApplication;component/Images/technical_icon.png��$��50��$��50���� $��	Técnicos��$ Center=��� -   $ btnManageTreatmentPrices��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $C��=/MyDentApplication;component/Images/treatment_prices_icon.png��$��50��$��50���� $ Center=��� $�Costos ���� $�de���� �� $�tratamientos���� -   $ btnConfigureEmail��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $A��;/MyDentApplication;component/Images/configureEmail_icon.png��$��50��$��50���� $ Center=��� $�
Configurar���� �� $�correo���� -   $ btnManageDotations��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $;��5/MyDentApplication;component/Images/dotation_icon.png��$��50��$��50���� $ Center=�$��
Dotaciones���� -   $ btnManageInventory��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $<��6/MyDentApplication;component/Images/inventory_icon.png��$��50��$��50���� $��
Inventario��$ Center=��� -   $ btnManageReceivedInvoices��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $C��=/MyDentApplication;component/Images/received_invoice_icon.png��$��50��$��50���� $ Center=��� $�Facturas���� �� $�	recibidas���� -   $ btnManageOutgoingInvoices��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $C��=/MyDentApplication;component/Images/outgoing_invoice_icon.png��$��50��$��50���� $ Center=��� $�
Recibos de���� �� $�
honorarios���� -   $ btnTotalInvoices��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $:��4/MyDentApplication;component/Images/invoice_icon.png��$��50��$��50���� $ Center=��� $�Facturación���� -   $ btnManageBanks��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $7��1/MyDentApplication;component/Images/bank_icon.png��$��50��$��50���� $ Center=��� $�Bancos���� -   $ btnManageAuthorizations��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $@��:/MyDentApplication;component/Images/authorization_icon.gif��$��50��$��50���� $ Center=�$��Autorizaciones���� -   $ btnManageElegibilities��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $>��8/MyDentApplication;component/Images/elegibility_icon.png��$��50��$��50���� $ Center=�$��Elegibilidades���� -   $ gbMedicines��$%��Medicamento que caduca este mes��$��0,10,237,41.001q�$
��Bold"�$��2q�
���   �
���   �$��Right=�$	��222��.��� -   $ gridMedicines��$��0,10,-2,2.245q�.+��� -   $	 rcExpiredMedicine��$��20��$��0,7.332,184.66,0q�
���   �$	��Top=�$��Right=�$��20������ $
 0.5,1&�$ 0.5,0&�.��� $ 	#FFEA5A5A��� $ 	#FFE0E0E0��$	 0.5Q�� $ 	#FFEA5A5A��$ 1Q��� -    $	 rcReplacedMedicine��$��20��$��0,39.385,184.66,0q�
���   �$	��Top=�$��Right=�$��20������ $
 0.5,1&�$ 0.5,0&�.��� $ 	#FF5AEA61��� $ 	#FFE0E0E0��$	 0.5Q�� $ 	#FF5AEA61��$ 1Q��� -!   $ lblExpiredMedicine��$��Sin reemplazar��$��27��$��0,4.05,10,0q�$	��Top=�$��Right=�$��174.66��$��Normal"��� -"   $ lblReplacedMedicine��$��Reemplazado��$��27��$��0,36.05,10,0q�$	��Top=�$��Right=�$��174.66��$��Normal"��� -#   $ svMedicines��$��12,68.05,10,32q�
���   �$��0q�$��Normal"�$
��Auto=�.���� -$   $ spMedicines���� -%   $ gbDotations��$ ��Dotaciones del día de hoy��$��0,8.55,464,42.451q�$
��Bold"�$��2q�
���   �
���   �$��Right=�$	��222��.��� -&   $ gridDotations��$��0,10,-2,2.245q�.+��� -'   $	 rcPendingDotations��$��20��$��0,7.332,184.66,0q�
���   �$	��Top=�$��Right=�$��20������ $
 0.5,1&�$ 0.5,0&�.��� $ 	#FFEA5A5A��� $ 	#FFE0E0E0��$	 0.5Q�� $ 	#FFEA5A5A��$ 1Q��� -(   $	 rcSignedDotations��$��20��$��0,39.385,184.66,0q�
���   �$	��Top=�$��Right=�$��20������ $
 0.5,1&�$ 0.5,0&�.��� $ 	#FF5AEA61��� $ 	#FFE0E0E0��$	 0.5Q�� $ 	#FF5AEA61��$ 1Q��� -)   $ lblPendingDotations��$��
Sin firmar��$��27��$��0,4.05,10,0q�$	��Top=�$��Right=�$��174.66��$��Normal"��� -*   $ lblSignedDotations��$��Firmadas��$��27��$��0,36.05,10,0q�$	��Top=�$��Right=�$��174.66��$��Normal"��� -+   $ svDotations��$��12,68.05,10,32q�
���   �$��0q�$��Normal"�$
��Auto=�.���� -,   $ spDotations���� --   $ gbAdministration_Copy��$�� ��$
��Left=�$��10,10,0,102.001q�$��228.5��$
��Bold"�
���   �$��2q�.���� $��0,10,-2,2.019q�$
��Auto=�$��Normal"�$��Center=�.���� $ 
Horizontal=�.+��� $
��Left=�$	��Top=�$��15,0,5,0q�.+��� -.   $ btnOpenAgenda��$��0,0,0,5q�$��90��$��90��$
 Hand��$��Center=�$��Center=�.���� $ Vertical=�.+��� $9��3/MyDentApplication;component/Images/agenda_icon.png��$��50��$��50���� $ Center=��� $�Agenda���� $	� de���� �� $�citas���� -/   $ btnOpenCashRegister��$��0,0,0,5q�$��90��$��90��$
 Hand��$��Center=�$��Center=�.���� $ Vertical=�.+��� $@��:/MyDentApplication;component/Images/cash_register_icon.png��$��50��$��50���� $ Center=��� $
�Caja���� -0   $ btnManageBudgets��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $9��3/MyDentApplication;component/Images/budget_icon.png��$��50��$��50���� $��Presupuestos��$ Center=��� -1   $ btnManageTreatments��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $<��6/MyDentApplication;component/Images/treatment_icon.png��$��50��$��50���� $ Center=��� $�Tratamientos���� -2   $ btnManageReminders��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $;��5/MyDentApplication;component/Images/reminder_icon.png��$��50��$��50���� $��Recordatorios��$ Center=��� -3   $ btnManageLaboratoryWorks��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $A��;/MyDentApplication;component/Images/laboratoryWork_icon.png��$��50��$��50���� $ Center=�$��34.627���� $�Trabajos de ���� �� $�laboratorio���� -4   $ btnManageContacts��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $;��5/MyDentApplication;component/Images/contacts_icon.png��$��50��$��50���� $ Center=��� $�Agenda���� $	� de���� �� $�	contactos���� -5   $# btnManageAmericanExpressPaids��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $C��=/MyDentApplication;component/Images/american_express_icon.png��$��50��$��50���� $ Center=��� $�Pagos American ���� �� $�Express���� -6   $ btnManageProviders��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $;��5/MyDentApplication;component/Images/provider_icon.png��$��50��$��50���� $��Proveedores��$ Center=��� .+��� -7   $ btnViewEvents��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $E��?/MyDentApplication;component/Images/view_event_history_icon.png��$��50��$��50���� $ Center=��� $�Historial de���� �� $�citas���� -8   $ btnViewPaymentFolios��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $>��8/MyDentApplication;component/Images/view_folios_icon.png��$��50��$��50���� $ Center=��� $�Historial de ���� �� $�folios���� -9   $ btnPendingPayments��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $A��;/MyDentApplication;component/Images/pendingPayment_icon.png��$��50��$��50���� $ Center=��� $�Saldos ���� �� $�
pendientes���� -:   $ btnManagePatients��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $:��4/MyDentApplication;component/Images/patient_icon.png��$��50��$��50���� $ Center=�$��	Pacientes���� -;   $ btnManageMedicines��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $;��5/MyDentApplication;component/Images/medicine_icon.png��$��50��$��50���� $ Center=��� $�Materiales y���� �� $�m���� $�edicamentos���� -<   $ btnManageCleanedMaterials��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $C��=/MyDentApplication;component/Images/cleanedMaterials_icon.png��$��50��$��50���� $��Esterilizado��$ Center=��� -=   $ btnGeneralPaids��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $@��:/MyDentApplication;component/Images/general_paids_icon.png��$��50��$��50���� $ Center=��� $�Gastos���� �� $�	generales���� ->   $ btnPrintDocuments��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $8��2/MyDentApplication;component/Images/print_icon.png��$��50��$��50���� $ Center=�$��Justificantes���� -?   $ btnSendMails��$��90��$��90��$
 Hand��$��Center=�$��Center=�$��0,0,0,5q�.���� $ Vertical=�.+��� $<��6/MyDentApplication;component/Images/sendEmail_icon.png��$��50��$��50���� $��Enviar correos��$ Center=��� $ ��404.5,372.999,409.5,26.001q�$;��5/MyDentApplication;component/Images/mydent_logo_1.jpg��$
��Fill=� K� ZIndex$ -1�� J� Opacity$	 0.6Q��� $��387,91,399,293q�$;��5/MyDentApplication;component/Images/mydent_logo_2.jpg��$
��Fill=�$ -1��$ 0.595Q�