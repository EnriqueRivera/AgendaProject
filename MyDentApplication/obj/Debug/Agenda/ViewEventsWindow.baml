   M S B A M L   `   `   `  ����L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  5      (    "MyDentApplication.ViewEventsWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      1localclr-namespace:MyDentApplication     5      -   5          �Name$  Window��5       � Title$ Citas��5      $	��775��5      $��502.119��6    � WindowStartupLocation$ CenterScreen=�6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6D    � 	Resources 5
      ;   5MyDentApplication.EventSatusAndFullDateValueConverter '  #EventSatusAndFullDateValueConverter��%   &	          5   
   64   5      .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5      (  "System.Windows.Controls.DatePicker  -   5          Name$ dpStartDate��5      $��212.64��6$   $	��Top=�63   $��
109,43,0,0q�6K   $
��Left=�6_     5      -   5        $ 	dpEndDate��5      $��212.64��6"   $	��Top=�61   $��
109,73,0,0q�6I   $
��Left=�6]   �� 5      $��93��6
   $	��Top=�6   $��	11,43,0,0q�6-   $��24��6@   $
��Left=�6L   $��Fecha de inicio:��6g   �� 5      $��77.667��6
   $	��Top=�6   $��26.333,72,0,0q�61   $��24��6H   $
��Left=�6T   $��Fecha de fin:��6o   �� 5      -   5         ��Name$ 
lblPatient��5   
   $��61.581��6   $	��Top=�6-   $��42.419,12,0,0q�6E   $��27��6\   $
��Left=�6h   $��	Paciente:��6�   �� 5      -   5         ��Name$ 
cbPatients��5      $��212.64��6!   $	��Top=�60   $��1��6H   $��
109,12,0,0q�6U   $��26��6i   $
��Left=�6u   $��Normal"�6�   �� 5      -   5        	 ��Name$	 	btnFilter��5      $
��68.5��6   $	��Top=�6+   $��7��6C   $��346.14,41.48,0,0q�6P   $��29��6j   $
��Left=�6v   $��Filtrar��6�   
 � Cursor$

 Hand��6�   &   System.Windows.Controls.DataGrid  5      -   5          Name$ dgEvents��5      $��1��6      SelectionMode$ Single=�6,   $��10,105.75,10,10q�6C      
IsReadOnly .    CanUserAddRows .     CanUserSortColumns .    CanUserDeleteRows .     AutoGenerateColumns .  ��6\   �� *ObservableData+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ No. de cita��65      Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.11* 6J   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6   �� *	EventId+6J     5      $ Inicia��6   $
 0.2* 6)    5      `� 5       `� 	Converter# [  6   .1�5   	   ��  �� Path$ 
StartEvent�6   5   	   5        5       $ Termina��6   $
 0.2* 6*    5!      `� 5"      # [  6   .1�5#   	   �� $ EndEvent�6   5$   	   5&        5'      $ Estado��6   $ 0.12* 6)    5(      `� 5)      # [  6'   .1�5*   	   �� $ IsException�6   �� 5+   	   $ 
IsCanceled�6   �� 5,   	   $ IsCompleted�6   �� 5-   	   $ PatientSkips�6   �� 5.   	   $ IsConfirmed�6   5/   	   51        52      $ Paciente��6   $ 0.25* 6+    53      `� 54       �� StringFormat$ (Exp. No. {0}) {1} {2}��6   .1�55   	   �� $ Patient.AssignedId�6   �� 56   	   $ Patient.FirstName�6   �� 57   	   $ Patient.LastName�6   58   	   5:        5;      $ Tratamiento��6<   $ 0.19* 6Q    6   �� *Treatment.Name+6Q   5=      5>      5?      