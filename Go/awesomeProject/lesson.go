package main

import "fmt"

// 一番最初に呼ばれる関数
func init() {
	fmt.Println("Init!")
}

func buzz() {
	fmt.Println("Bazz")
}

// initの後に呼ばれる関数
func main() {
	buzz()
	fmt.Println("Hello world!", "TEST TEST")
}
