   M S B A M L   `   `   `  ����   PomodoroPlusDetails$    PomodoroPlusDetails.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Q MElysium, Version=2.0.1042.4, Culture=neutral, PublicKeyToken=afa220db249e5b15/metro#http://schemas.codeplex.com/elysium    � Title$  
Data Entry��$	��260��$	��460��.���  � RowDefinitions �� $
��Auto ��� $��* �.+��� $��0�� �� Orientation$ 
Horizontal=�.+�o� -    o�Name$ MyMenu��.@�m� -    m�Name$ refreshMenuItem��$��20��$��5,0,5,0q�$��0q�$��Refresh��(  "System.Windows.Controls.DatePicker  $��0��$��Center=�   SelectedDate �� *SelectedDate+&   System.Windows.Controls.DataGrid  -     Name$ 	ItemsGrid��$��1��   AutoGenerateColumns .     CanUserAddRows .  	   CanUserDeleteRows	 .  ���� *
TimeSlices+
   Columns
 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Time��   
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	StartTime+ �� StringFormat$ t��  $ Duration�� .  �� *
Duration+$ 
 {0:d} min��  $
 Type�� .  �� *Type+  $ Description��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 270  �� *Description+