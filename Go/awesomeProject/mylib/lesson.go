package mylib

import (
	"fmt"
	"io"
	"log"
	"os"
)

func LoggingSettings(logFile string) {
	logfile, _ := os.OpenFile(logFile, os.O_RDWR|os.O_CREATE|os.O_APPEND, 0666)
	multuLogFile := io.MultiWriter(os.Stdout, logfile)
	log.SetFlags(log.Ldate | log.Ltime | log.Llongfile)
	log.SetOutput(multuLogFile)
}

func channelExample() {
	ch := make(chan int, 2)
	println("LEN", len(ch))
	ch <- 100
	ch <- 200
	println("LEN", len(ch))
	x := <-ch
	println("x", x)
	println("LEN", len(ch))
	y := <-ch
	println(y)
	println("y", "LEN", len(ch))
}

func q1() int {
	f := 1.11
	fmt.Println(f)
	return int(f)
}

func q1_2() {
	l := []int{100, 300, 23, 11, 23, 2, 4, 6, 4}
	m := 0
	for _, v := range l {
		if m <= v {
			m = v
		}
	}
	fmt.Println(m)
}

func q2_2() {
	m := map[string]int{
		"apple":  200,
		"banana": 300,
		"grapes": 150,
		"orange": 80,
		"papaya": 500,
		"kiwi":   90,
	}
	sum := 0
	for _, v := range m {
		sum += v
	}
	fmt.Println("Total price:", sum)
}

func q1_3() {
	var i int = 10
	var p *int
	p = &i
	var j int
	j = *p
	fmt.Println(j)
}

func q2_3() {
	var i int = 100
	var j int = 200
	var p1 *int
	var p2 *int
	p1 = &i
	p2 = &j
	i = *p1 + *p2
	fmt.Println(i)
	fmt.Println(*p1, *p2)
	p2 = p1
	fmt.Println(*p1, *p2)
	j = *p2 + i
	fmt.Println(j)
}

// メソッド
func (v Vertex_1) Plus() int {
	return v.X + v.Y
}

// Vertex型の定義
type Vertex_1 struct {
	X, Y int
}

// メソッドの呼び出し
func q1_4() {
	v := Vertex_1{3, 4}
	fmt.Println(v.Plus())
}

func (v Vertex_2) String() string {
	return fmt.Sprintf("X is %d! Y is %d!", v.X, v.Y)
}

type Vertex_2 struct {
	X, Y int
}

func q2_4() {
	v := Vertex_2{3, 4}
	fmt.Println(v)
}

func q3() {
	m := map[string]int{"Mike": 20, "Nancy": 24, "Messi": 30}
	fmt.Printf("%T %v", m, m)
}

func goroutine(s []string, c chan string) {
	sum := ""
	for _, v := range s {
		sum += v
		c <- sum
	}
	close(c)
}

func q4() {
	words := []string{"test1!", "test2!", "test3!", "test4!"}
	c := make(chan string)
	go goroutine(words, c)

	for w := range c {
		fmt.Println(w)
	}
}

func lesson() {
	q4()
	// channelExample()
	// q1_3()
	// q2_3()
	// q1_4()
	// q2_4()
	// LoggingSettings("test.log")
	// file, err := os.Open("./lesson.go")
	// if err != nil {
	// 	log.Fatal("Error opening file")
	// }
	// defer file.Close()
	// data := make([]byte, 100)
	// count, err := file.Read(data)
	// if err != nil {
	// 	log.Fatal("Error reading file")
	// }
	// fmt.Printf("Read %d bytes: %q\n", count, data[:count])
	// log.Fatal("Fatal error. Program stopped")
	// fmt.Println(q1())
	// q2 = 5,6
	// q3()

	//c := make([]int, 5)
	// c := make([]int, 0, 5)
	// for i := 0; i < 5; i++ {
	// 	c = append(c, i)
	// 	fmt.Println(c)
	// }
	// fmt.Println(c)
}
