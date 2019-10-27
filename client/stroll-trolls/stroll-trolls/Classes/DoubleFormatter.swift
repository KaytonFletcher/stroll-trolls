//
//  DoubleFormatter.swift
//  stroll-trolls
//
//  Created by Kayton Fletcher on 10/26/19.
//  Copyright © 2019 Kayton Fletcher. All rights reserved.
//

import Foundation

public class DoubleFormatter: Formatter {

    override public func string(for obj: Any?) -> String? {
        var retVal: String?
        let formatter = NumberFormatter()
        formatter.numberStyle = .decimal

        if let dbl = obj as? Double {
            retVal = formatter.string(from: NSNumber(value: dbl))
        } else {
            retVal = nil
        }

        return retVal
    }

    override public func getObjectValue(_ obj: AutoreleasingUnsafeMutablePointer<AnyObject?>?, for string: String, errorDescription error: AutoreleasingUnsafeMutablePointer<NSString?>?) -> Bool {

        var retVal = true

        if let dbl = Double(string), let objok = obj {
            objok.pointee = dbl as AnyObject?
            retVal = true
        } else {
            retVal = false
        }

        return retVal

    }
}
