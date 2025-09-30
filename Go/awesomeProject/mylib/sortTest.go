package mylib

import (
	"fmt"
	"sort"
)

func SortLesson() {
	i := []int{5, 2, 6, 3, 1, 4}
	s := []string{"c", "a", "e", "b", "d"}
	p := []struct {
		Name string
		Age  int
	}{
		{"Bob", 31},
		{"John", 42},
		{"Michael", 17},
		{"Jenny", 26},
	}
	fmt.Println(i, s, p)
	sort.Ints(i)
	sort.Strings(s)
	sort.Slice(p, func(i, j int) bool {
		return p[i].Name < p[j].Name
	})
	sort.Slice(p, func(i, j int) bool {
		return p[i].Age < p[j].Age
	})
	fmt.Println(i, s, p)

}
