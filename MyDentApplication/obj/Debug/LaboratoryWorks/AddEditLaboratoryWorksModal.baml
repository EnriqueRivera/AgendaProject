   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5       MyDentApplication3   -MyDentApplication.AddEditLaboratoryWorksModal   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      9System&clr-namespace:System;assembly=mscorlib     5      -   5          �Name$  Window��5       � Title$$ Agregar trabajo de laboratorio��5      $	��390��5      $	��299��6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6    � 
ResizeMode$ CanMinimize=�6S    � WindowStartupLocation$ CenterScreen=�6l   .��5
      � -   5         �Name$ 
LayoutRoot��5
      .+�5      �� -   5         ��Name$ 
lblPatient��5   
   $��	Paciente:��6   $
��Left=�62   $��27��6M   $��85.5,12,0,0q�6Y   $	��Top=�6n   $
��62.5��6�   �� 5      -   5        $ lblWorkName��5   
   $��Trabajo:��6   $
��Left=�62   $��27��6M   $��96.167,45,0,0q�6Y   $	��Top=�6p   $��51.833��6�   �� 5      -   5         ��Name$  btnAddUpdateLaboratoryWork��5      $��Agregar��6/   $
��Left=�6A   $��29��6\   $��90.67,214.197,0,0q�6h   $	��Top=�6�   $��91��6�    � Cursor$
 Hand��6�   $��7��6�   �� 5      -   5        $ 	btnCancel��5      $��Cancelar��6   $
��Left=�61   $��29��6L   $��199.67,214.197,0,0q�6X   $	��Top=�6t   $��91��6�   $
 Hand��6�   $��8��6�   �� 5      -   5        $ lblReceivedDate��5   
   $��Fecha de recepción:��6#   $
��Left=�6A   $��27��6\   $��31,136.668,0,0q�6h   $	��Top=�6�   $	��117��6�   �� 5      -   5        	 ��Name$	 
cbPatients��5      $��26��6!   $��
153,13,0,0q�6-   $	��Top=�6A   $��Normal"�6Y   $��1��6m   $
��Left=�6z   $��194.5��6�   (  "System.Windows.Controls.DatePicker  5      -	   5        
  Name$
 dtpReceivedDate��5      $
��Left=�6(   $��27��6C   $��153,136.668,0,0q�6O   $	��Top=�6h   $��194.5��6�   $��2��6�   {�.  6�   �� 5      -
   5        $ lblDeliveryDate��5   
   $��Fecha de entrega:��6#   $
��Left=�6?   $��27��6Z   $��41.667,76,0,0q�6f   $	��Top=�6}   $��106.333��6�     5      -   5        $
 dtpDeliveryDate��5      $
��Left=�6(   $��27��6C   $��
153,77,0,0q�6O   $	��Top=�6c   $��194.5��6{   $��3��6�   �� 5      -   5         ��Name$ txtWorkName��5      $
��Left=�6!   $��27��6<   $��
153,44,0,0q�6H    �� TextWrapping$
 Wrap=�6\   $	��Top=�6p   $��194.5��6�   $��4��6�   $��1q�6�   
���   �6�   �� 5      -   5        $ lblTechnical��5   
   $��	Técnico:��6    $
��Left=�63   $��27��6N   $��93.003,168.668,0,0q�6Z   $	��Top=�6v   $��54.997��6�   �� 5      -   5        $	 cbTechnical��5      $��26��6"   $��153,168.668,0,0q�6.   $	��Top=�6G   $��Normal"�6_   $��5��6s   $
��Left=�6�   $��194.5��6�   �� 5      -   5        $ lblReceivedWork��5   
   $��Trabajo recibido:��6#   $
��Left=�6?   $��27��6Z   $��
47,108,0,0q�6f   $	��Top=�6z   $	��101��6�   �� 5      -   5         ��Name$ chkReceivedWork��5      $�� ��6&   $
��Left=�61   $��18��6L   $��153,113.668,0,0q�6X   $	��Top=�6q   $��15.333��6�   6�   5      5      