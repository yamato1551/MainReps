package mylib

import (
	"fmt"
	"testing"
)

func Example() {

}

func ExampleAverage() {
	v := Average([]int{1, 2, 3, 4, 5})
	fmt.Println(v)
}

func ExamplePerson2_Say() {
	p := Person2{Name: "Alice", Age: 30}
	p.Say()
	// Output: Person2
}

func TestAverage(t *testing.T) {
	t.Skip("Skipping TestAverage")
	v := Average([]int{1, 2, 3, 4, 5})
	if v != 3 {
		t.Errorf("Average failed, got %v, want 3", v)
	}
}
