def RectangulT(width,height):
    if width == height or abs(int(width) - int(height))>=5:
        print('The area of the rectangular is: ' ,int(width) * int(height))
    else:
        print('The perimeter of the rectangular is: ',2*int(width)+2*int(height))



def TriangleT(width,height):
    choice = input("Enter your choice 1- calculates the area of the triangle 2- print the triangle")
    if choice == "1":
        print('The area of the triangle is: ' ,int(width)+ 2*int(height))
    elif choice == "2":
        if int(width)%2 ==0 or int(width)> 2*int(height):
            print('The triangle cannot be printed')
        else:
            print_triangle(width, height)

    else:
        print("Invalid choice")



def rec1(numdiffrows, numperow, w, space, width):
    if numdiffrows > 0:
        rec2(numperow, w, space)
        w += 2
        space = (int(width)- int(w)) / 2
        rec1(numdiffrows - 1, numperow, w, space, width)


def rec2(numperow, w, space):
    if numperow > 0:
        print(" " * int(space) + "*" * int(w))
        rec2(numperow - 1, w, space)


def print_triangle(width, height):
    w = 1
    space = (int(width)- int(w)) / 2
    print(" " * int(space) + "*" * int(w))
    w = 3
    space = (int(width) - w) / 2
    numdiffrows = (int(width)- 3) / 2
    if not (int(height) - 2) % int(numdiffrows)== 0:
        print(" " * int(space) + "*" * int(w))
    numperow = int(int(height)- 2) / int(numdiffrows)
    rec1(numdiffrows, numperow, w, space, width)
    print("*" * int(width))



def main():
    while True:
        choice = input("Enter your choice (1, 2, or 3): ")
        if choice == "3":
            print("Exiting the loop")
            break
        elif choice == "1":
            print("You chose rectangular tower")
            print("Enter width and height")
            width = input()
            height = input()
            RectangulT(width,height)
        elif choice == "2":
            print("You chose triangle tower")
            print("Enter width and height")
            width = input()
            height = input()
            TriangleT(width,height)
        else:
            print("Invalid choice")


if __name__ == "__main__":
    main()

'''for item in range(int(numdiffrows)):
    for item in range(int(numperow)):
        print(" " * int(space) + "*" * int(w))
        w+=2
        space = (width - w)/2'''