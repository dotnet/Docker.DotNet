package main

import (
	"errors"
	"strings"
)

const (
	Header = "header"
	Body   = "body"
	Query  = "query"

	InTag       = "in"
	NameTag     = "name"
	RequiredTag = "required"
)

type RestTag struct {
	In       string
	Name     string
	Required bool
}

func RestTagFromString(tag string) (RestTag, error) {
	if tag == "" {
		return RestTag{}, errors.New("Nil or empty tag string.")
	}

	r := RestTag{In: "", Name: "", Required: false}
	requiresName := false

	entries := strings.Split(tag, ",")
	for _, entry := range entries {
		decls := strings.Split(entry, ":")
		if len(decls) == 2 {
			tag, value := decls[0], decls[1]
			switch tag {
			case InTag:
				switch value {
				case Header:
					r.In = value
					requiresName = true
				case Body:
					r.In = value
				case Query:
					r.In = value
					requiresName = true
				default:
					return RestTag{}, errors.New("Incorrect 'in' value: " + value)
				}
			case NameTag:
				r.Name = value
			default:
				return RestTag{}, errors.New("Unknown 2 part tag:value of: " + entry)
			}
		} else if entry == RequiredTag {
			r.Required = true
		} else {
			return RestTag{}, errors.New("Unknown entry: " + entry)
		}
	}

	if r.In == "" {
		return RestTag{}, errors.New("in: tag is always required.")
	}

	if requiresName && r.Name == "" {
		return RestTag{}, errors.New("in: tag required name: tag.")
	}

	return r, nil
}
