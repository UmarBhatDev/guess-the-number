
# Preview gif: 
![](https://github.com/UmarBhatDev/guess-the-number/blob/main/PreviewResources/Preview_GIF.gif)

The gif is laggin though (it is not the actual FPS obviously). For actuall footage click [here](https://drive.google.com/file/d/1IcpolEnlK4ZfybGZ8OVwTYFY-fn0yUjt/view?usp=sharing).

.
.
.
.
.

### > _При неправильном ответе показываем больше или меньше загаданного._

Вот эта штука после ответа показывает больше он или меньше заданного числа. При этом показывает она статус только последнего ответа.

![](https://github.com/UmarBhatDev/guess-the-number/blob/main/PreviewResources/Preview_BiggerSmaller_Sign.jpg)

### > _Ввод числа должен осуществляться путем нажатия кнопок с цифрами в интерфейсе игры._

Минус ставить перед числом необязательно. Просто нажав минус число домножается на -1. В клавиатуре нет ограничений на ввод. Если в игре диапазон [-10, 10] игрок все еще может веcти 999999.

![](https://github.com/UmarBhatDev/guess-the-number/blob/main/PreviewResources/Preview_InGameKeyboard.jpg)

### > _После каждого ответа, расположение цифр должно меняться на случайное._
Использовал алгоритм перетасовки Фишера-Йетса. O(n)

### > _Результат ответа игроков должен отображаться в интерфейсе игры._
Тут вертикальный бар, в который элементы добавялются снизу вверх. Анимации нет, к сожалению. Зато есть простяцкий пул объектов:)

![](https://github.com/UmarBhatDev/guess-the-number/blob/main/PreviewResources/Preivew_History_Bar.jpg)
