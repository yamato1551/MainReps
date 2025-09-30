/*
mylib is my original library.
*/
package mylib

import "fmt"

// Person2 doc
type Person2 struct {
	// Name
	Name string
	// Age
	Age int
}

// Say doc
func (p *Person2) Say() {
	fmt.Println("Person2")
}

// Average calculates the average of a slice of integers.
func Average(s []int) int {
	total := 0
	for _, i := range s {
		total += i
	}
	// 平均値計算
	return int(total / len(s))
}
