   M S B A M L   `   `   `  ����?  ;avto, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:avtoavto  5   	       avto.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:avto     5   	   -   5          �Name$  window1��5   5    � Title$ 
MainWindow��6	   $	��400��6   $	��474��6)    � WindowStartupLocation$ CenterScreen=�5
       � 
ResizeMode$ NoResize=�6C   .��5      � ���	#FF83EEEE � ColumnDefinitions 5   
   �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      .+�5   
   �� -   5         ��Name$ LoginTextBox��5       �� TextAlignment$ Center=�6   $��Center=�62   $��10(�6T   $��97,140,97,10q�6b   $��0��6x   $��3��6�   G� 5   
   -   5         G�Name$ PasswordBox��5      $��1��6   $��10(�6(   $��Center=�67   $��Center=�6Y   $��97,30,97,120q�6~   $��3��6�   �� 5   
   -   5        	 ��Name$	 button1��5   M   $��18(�6   $*��$Зарегистрироваться��6   $��0��6<   $��
5,5,90,140q�6t   $��2��6�   ��5      �� 5      
 �� Color$
 White��6"    �� Opacity$	 0.5Q�60   5      �� 5   
   -   5        $	 button3��5   �   $��18(�6   $��1��6   $��1��6/   $��Right=�6<   $��Bottom=�6X   $��0,0,5,5q�6s   $��Вход��6�   $	��213��6�   $��40��6�   $��2��6�   ��5      �� 5       $
 White��6"   $	 0.5Q�60   5"      �� 5$   
   -   5         ��Name$ label��5%      $��0��6   $��1��6%   $��5,100,50,50q�62   $��2��6H   �� 5&   
   -   5        $ labe2��5'      $	��143��6   $!��Введите Пароль��6   $��Center=�68   $��18(�6\   $��1��6j   $��1��6z   $��Center=�6�   $	��Top=�6�   �� 5(   
   -   5        $ labe3��5)      $	��156��6   $��Введите Логин��6   $��18(�68   $��Center=�6F   $��36��6k   $��1��6w   $��Center=�6�   $	��Top=�6�   $��	0,108,0,0q�6�   �� 5*   
   -	   5        $	 button2��5+   o   $@��:Изменить настройки подключения��6   $��13(�6-   $��0��6;   $��1��6K   $��4,140, 90,5q�6Z   $��2��6�   ��5,      �� 5-      $
 White��6"   $	 0.5Q�60   5/      �� 50   
   -
   5        $	 button4��51   Q   $��1��6   $��18(�6   $��
Выйти��6-   $��
94,5,5,140q�6=   $��2��6x   ��52      �� 53      $
 White��6"   $	 0.5Q�60   55      56      58      