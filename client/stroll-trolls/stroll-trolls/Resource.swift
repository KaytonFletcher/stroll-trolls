//
//  Resource.swift
//  stroll-trolls
//
//  Created by Kayton Fletcher on 10/27/19.
//  Copyright © 2019 Kayton Fletcher. All rights reserved.
//

import Foundation
import SwiftUI

struct User: Codable {
    var name: String
    var location: String?
}

func userInfo(login: String) -> Endpoint<User> {
    return Endpoint(json: .get, url: URL(string: "https://api.github.com/users/\(login)")!)
}

let sample = userInfo(login: "objcio")

final class Resource<A>: BindableObject {
    let didChange = PassthroughSubject<A?, Never>()
    let endpoint: Endpoint<A>
    var value: A? {
        didSet {
            DispatchQueue.main.async {
                self.didChange.send(self.value)
            }
        }
    }
    init(endpoint: Endpoint<A>) {
        self.endpoint = endpoint
        reload()
    }
    func reload() {
        URLSession.shared.load(endpoint) { result in
            self.value = try? result.get()
        }
    }
}
