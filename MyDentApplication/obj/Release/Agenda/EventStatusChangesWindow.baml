   M S B A M L   `   `   `  ���� L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  0    *MyDentApplication.EventStatusChangesWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     1localclr-namespace:MyDentApplication     -     �Name$  Window�� � Title$  Cambios de estado de citas��$	��640��$	��545�� � WindowStartupLocation$ CenterScreen=� � Icon$6 0/MyDentApplication;component/Images/app_icon.ico�� � 	Resources 7   1MyDentApplication.EventSatusAndDateValueConverter #  EventSatusAndDateValueConverter��%   &	          .��� -    �Name$ 
LayoutRoot��.+��� -    ��Name$ lblEventStartTimeCaption��$��Inicia:��$��68.866,38.926,0,0q�$��Normal"�$
��Left=�$��41.134��$��25��$	��Top=��� -    ��Name$ lblEventStartTime��$�� ��$
��Left=�$��20��$��115,43.926,0,0q�$	��Top=�$	��494��$��Normal"�$��Italic$��� -   $ lblEventEndTimeCaption��$��Termina:��$��54.478,63.926,0,0q�$��Normal"�$
��Left=�$��55.522��$��25��$	��Top=��� -   $ lblEventEndTime��$�� ��$
��Left=�$��20��$��115,68.926,0,0q�$	��Top=�$	��494��$��Normal"�$��Italic$��� -   $ lblEventStatusCaption��$��Estado actual:��$��26.478,88.926,0,0q�$��Normal"�$
��Left=�$��83.522��$��25��$	��Top=��� -   $ lblEventStatus��$�� ��$
��Left=�$��20��$��115,93.926,0,0q�$	��Top=�$	��494��$��Normal"�$��Italic$��� -	   $ lblEventCapturerCaption��$%��Usuario que
registró la cita:��$��20.478,113.926,0,0q�$��Normal"�$
��Left=�$��89.522��$��42��$	��Top=��� -
   $ lblEventCapturer��$�� ��$��115,126.926,0,0q�$��Normal"�$��Italic$�$��20��$	��Top=�$
��Left=�$	��494���� -   $ lblEventIdCaption��$��Cita #��$��68.866,13.926,0,0q�$��Normal"�$
��Left=�$��41.134��$��25��$	��Top=��� -   $ 
lblEventId��$
��Left=�$��20��$��115,18.5,0,0q�$	��Top=�$	��494��$��Normal"�$��Italic$�&   System.Windows.Controls.DataGrid  $��11.5,167.593,10,21q�   
IsReadOnly . 	   AutoGenerateColumns	 .  
   SelectionMode$
 Single=�   CanUserAddRows .     CanUserDeleteRows .  $��1������ *ObservableData+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Estado anterior��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.1* 1  +System.Windows.Controls.DataGridBoundColumn   Binding `�  `� 	Converter# [  .1���  �� Path$ 	OldStatus�  $ Nuevo estado��$
 0.1*  `� # [  .1��� $ 	NewStatus�  $ Fecha del cambio��$ 0.15*  `� # [  .1��� $ 
ChangeDate�  $$ Usuario que realizó el cambio��$ 0.15*  `�  �� StringFormat$ {0} {1}��.1��� $ User.FirstName��� $ User.LastName�