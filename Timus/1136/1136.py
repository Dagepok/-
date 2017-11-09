class Parl:
    def __init__(self, parent, value):
        self.Parent = parent;
        self.Left = None;
        self.Right = None;
        self.Value = value;


def main():
    N = int(input())
    data = []
    for i in range(N):
        data.append(int(input()))
    data.reverse();
    head = Parl(None, data[0])
    for i in range(1, N):
                sCheckHead(data[i], head)
    result = []
    sortP(head, result)
    for i in result:
        print(str(i) + " ")


def CheckHead(number, head):
    if number > head.Value:
        if head.Right is not None:
            CheckHead(number, head.Right)
        else:
            head.Right = Parl(head, number)
    if number < head.Value:
         if head.Left is not None:
            CheckHead(number, head.Left)
        else:
            head.Left = Parl(head, number)


def sortP(head, result):
    if head.Right is not None:
        sortP(head.Right, result)
    if head.Left is not None:
        sortP(head.Left, result)
    result.append(head.Value)

if __name__ == "__main__":
    main()
