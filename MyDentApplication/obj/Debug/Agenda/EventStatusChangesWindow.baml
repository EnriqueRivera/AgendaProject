   M S B A M L   `   `   `  ����L  HMyDentApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyDentApplicationMyDentApplication  5      0    *MyDentApplication.EventStatusChangesWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      1localclr-namespace:MyDentApplication     5      -   5          �Name$  Window��5       � Title$  Cambios de estado de citas��5      $	��640��5      $	��545��6    � WindowStartupLocation$ CenterScreen=�6    � Icon$6 0/MyDentApplication;component/Images/app_icon.ico��6@    � 	Resources 5
      7   1MyDentApplication.EventSatusAndDateValueConverter #  EventSatusAndDateValueConverter��%   &	          5      6*   5      .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5      �� -   5         ��Name$ lblEventStartTimeCaption��5   
   $��Inicia:��6,   $��68.866,38.926,0,0q�6>   $��Normal"�6Y   $
��Left=�6m   $��41.134��6�   $��25��6�   $	��Top=�6�   �� 5      -   5         ��Name$ lblEventStartTime��5      $�� ��6)   $
��Left=�61   $��20��6L   $��115,43.926,0,0q�6X   $	��Top=�6p   $	��494��6�   $��Normal"�6�   $��Italic$�6�   �� 5      -   5        $ lblEventEndTimeCaption��5   
   $��Termina:��6*   $��54.478,63.926,0,0q�6=   $��Normal"�6X   $
��Left=�6l   $��55.522��6�   $��25��6�   $	��Top=�6�   �� 5      -   5        $ lblEventEndTime��5      $�� ��6'   $
��Left=�6/   $��20��6J   $��115,68.926,0,0q�6V   $	��Top=�6n   $	��494��6�   $��Normal"�6�   $��Italic$�6�   �� 5      -   5        $ lblEventStatusCaption��5   
   $��Estado actual:��6)   $��26.478,88.926,0,0q�6B   $��Normal"�6]   $
��Left=�6q   $��83.522��6�   $��25��6�   $	��Top=�6�   �� 5      -   5        $ lblEventStatus��5      $�� ��6&   $
��Left=�6.   $��20��6I   $��115,93.926,0,0q�6U   $	��Top=�6m   $	��494��6�   $��Normal"�6�   $��Italic$�6�   �� 5      -	   5        $ lblEventCapturerCaption��5   
   $%��Usuario que
registró la cita:��6+   $��20.478,113.926,0,0q�6\   $��Normal"�6x   $
��Left=�6�   $��89.522��6�   $��42��6�   $	��Top=�6�   �� 5      -
   5        $ lblEventCapturer��5      $�� ��6(   $��115,126.926,0,0q�60   $��Normal"�6I   $��Italic$�6]   $��20��6p   $	��Top=�6|   $
��Left=�6�   $	��494��6�   �� 5      -   5        $ lblEventIdCaption��5   
   $��Cita #��6%   $��68.866,13.926,0,0q�66   $��Normal"�6Q   $
��Left=�6e   $��41.134��6�   $��25��6�   $	��Top=�6�   �� 5      -   5        $ 
lblEventId��5      $
��Left=�6"   $��20��6=   $��115,18.5,0,0q�6I   $	��Top=�6_   $	��494��6w   $��Normal"�6�   $��Italic$�6�   &   System.Windows.Controls.DataGrid  5      $��11.5,167.593,10,21q�6      
IsReadOnly . 	   AutoGenerateColumns	 .  
   SelectionMode$
 Single=�5         CanUserAddRows .     CanUserDeleteRows .  $��1��6L   ��5      �� *ObservableData+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Estado anterior��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.1* 6>   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5      `� 5       `� 	Converter# [  6   .1�5    	   ��  �� Path$ 	OldStatus�6   5!   	   5#        5$      $ Nuevo estado��6%   $
 0.1* 6;    5%      `� 5&      # [  6   .1�5'   	   �� $ 	NewStatus�6   5(   	   5*        5+      $ Fecha del cambio��6%   $ 0.15* 6?    5,      `� 5-      # [  6   .1�5.   	   �� $ 
ChangeDate�6   5/   	   51        52      $$ Usuario que realizó el cambio��6   $ 0.15* 6@    53      `� 54       �� StringFormat$ {0} {1}��6   .1�55   	   �� $ User.FirstName�6   �� 56   	   $ User.LastName�6   57   	   59      5;      5<      5=      