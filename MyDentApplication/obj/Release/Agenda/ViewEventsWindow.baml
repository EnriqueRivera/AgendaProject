   M S B A M L   `   `   `  ���� L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  (    "MyDentApplication.ViewEventsWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     1localclr-namespace:MyDentApplication     -     �Name$  Window�� � Title$ Citas��$	��775��$��502.119�� � WindowStartupLocation$ CenterScreen=� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � 	Resources ;   5MyDentApplication.EventSatusAndFullDateValueConverter '  #EventSatusAndFullDateValueConverter��%   &	          .��� -    �Name$ 
LayoutRoot��.+�(  "System.Windows.Controls.DatePicker  -     Name$ dpStartDate��$��212.64��$	��Top=�$��
109,43,0,0q�$
��Left=�  -   $ 	dpEndDate��$��212.64��$	��Top=�$��
109,73,0,0q�$
��Left=��� $��93��$	��Top=�$��	11,43,0,0q�$��24��$
��Left=�$��Fecha de inicio:���� $��77.667��$	��Top=�$��26.333,72,0,0q�$��24��$
��Left=�$��Fecha de fin:���� -    ��Name$ 
lblPatient��$��61.581��$	��Top=�$��42.419,12,0,0q�$��27��$
��Left=�$��	Paciente:���� -    ��Name$ 
cbPatients��$��212.64��$	��Top=�$��1��$��
109,12,0,0q�$��26��$
��Left=�$��Normal"��� -   	 ��Name$	 	btnFilter��$
��68.5��$	��Top=�$��7��$��346.14,41.48,0,0q�$��29��$
��Left=�$��Filtrar��
 � Cursor$

 Hand��&   System.Windows.Controls.DataGrid  -     Name$ dgEvents��$��1��   SelectionMode$ Single=�$��10,105.75,10,10q�   
IsReadOnly .    CanUserAddRows .     CanUserSortColumns .    CanUserDeleteRows .     AutoGenerateColumns .  ���� *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ No. de cita��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.11* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	EventId+  $ Inicia��$
 0.2*  `�  `� 	Converter# [  .1���  �� Path$ 
StartEvent�  $ Termina��$
 0.2*  `� # [  .1��� $ EndEvent�  $ Estado��$ 0.12*  `� # [  .1��� $ IsException��� $ 
IsCanceled��� $ IsCompleted��� $ PatientSkips��� $ IsConfirmed�  $ Paciente��$ 0.25*  `�  �� StringFormat$ (Exp. No. {0}) {1} {2}��.1��� $ Patient.AssignedId��� $ Patient.FirstName��� $ Patient.LastName�  $ Tratamiento��$ 0.19*  �� *Treatment.Name+