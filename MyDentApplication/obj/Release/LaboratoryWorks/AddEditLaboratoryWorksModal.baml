   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem   MyDentApplication3   -MyDentApplication.AddEditLaboratoryWorksModal   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9System&clr-namespace:System;assembly=mscorlib     -     �Name$  Window�� � Title$$ Agregar trabajo de laboratorio��$	��390��$	��299�� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ 
LayoutRoot��.+��� -    ��Name$ 
lblPatient��$��	Paciente:��$
��Left=�$��27��$��85.5,12,0,0q�$	��Top=�$
��62.5���� -   $ lblWorkName��$��Trabajo:��$
��Left=�$��27��$��96.167,45,0,0q�$	��Top=�$��51.833���� -    ��Name$  btnAddUpdateLaboratoryWork��$��Agregar��$
��Left=�$��29��$��90.67,214.197,0,0q�$	��Top=�$��91�� � Cursor$
 Hand��$��7���� -   $ 	btnCancel��$��Cancelar��$
��Left=�$��29��$��199.67,214.197,0,0q�$	��Top=�$��91��$
 Hand��$��8���� -   $ lblReceivedDate��$��Fecha de recepción:��$
��Left=�$��27��$��31,136.668,0,0q�$	��Top=�$	��117���� -   	 ��Name$	 
cbPatients��$��26��$��
153,13,0,0q�$	��Top=�$��Normal"�$��1��$
��Left=�$��194.5��(  "System.Windows.Controls.DatePicker  -	   
  Name$
 dtpReceivedDate��$
��Left=�$��27��$��153,136.668,0,0q�$	��Top=�$��194.5��$��2��{�.  �� -
   $ lblDeliveryDate��$��Fecha de entrega:��$
��Left=�$��27��$��41.667,76,0,0q�$	��Top=�$��106.333��  -   $
 dtpDeliveryDate��$
��Left=�$��27��$��
153,77,0,0q�$	��Top=�$��194.5��$��3���� -    ��Name$ txtWorkName��$
��Left=�$��27��$��
153,44,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$��194.5��$��4��$��1q�
���   ��� -   $ lblTechnical��$��	Técnico:��$
��Left=�$��27��$��93.003,168.668,0,0q�$	��Top=�$��54.997���� -   $	 cbTechnical��$��26��$��153,168.668,0,0q�$	��Top=�$��Normal"�$��5��$
��Left=�$��194.5���� -   $ lblReceivedWork��$��Trabajo recibido:��$
��Left=�$��27��$��
47,108,0,0q�$	��Top=�$	��101���� -    ��Name$ chkReceivedWork��$�� ��$
��Left=�$��18��$��153,113.668,0,0q�$	��Top=�$��15.333��