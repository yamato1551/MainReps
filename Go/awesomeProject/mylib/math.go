package mylib

func Average(s []int) int {
	total := 0
	for _, i := range s {
		total += i
	}
	// 平均値計算
	return int(total / len(s))
}
