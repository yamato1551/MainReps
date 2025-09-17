package mylib

import "testing"

func TestAverage(t *testing.T) {
	t.Skip("Skipping TestAverage")
	v := Average([]int{1, 2, 3, 4, 5})
	if v != 3 {
		t.Errorf("Average failed, got %v, want 3", v)
	}
}
